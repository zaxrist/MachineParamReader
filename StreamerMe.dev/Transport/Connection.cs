using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StreamerMe.dev.Transport
{
    //From: https://github.com/dotnet/aspnetcore/blob/main/src/Servers/Kestrel/Transport.Sockets/src/Internal/SocketConnection.cs
    public class Connection : IAsyncDisposable
    {
        private const int MinBuffSize = 512;
        private readonly Socket _socket;
        private readonly Receiver _receiver;
        private Sender? _sender;
        private readonly SenderPool _senderPool;
        private Task? _receiveTask;
        private Task? _sendTask;
        private readonly Pipe _transportPipe;
        private readonly Pipe _applicationPipe;
        private readonly object _shutdownLock = new object();
        private volatile bool _socketDisposed;
        public PipeWriter Output { get; }
        public PipeReader Input { get; }

        public Connection(Socket socket, SenderPool senderPool,
            PipeScheduler transportScheduler, PipeScheduler applicationScheduler,
            MemoryPool<byte> memoryPool)
        {
            _socket = socket;
            _receiver = new Receiver();
            _senderPool = senderPool;
            _transportPipe = new Pipe(new PipeOptions(memoryPool,
                applicationScheduler, transportScheduler,
                useSynchronizationContext: false));
            Output = _transportPipe.Writer;
            _applicationPipe = new Pipe(new PipeOptions(memoryPool,
                transportScheduler, applicationScheduler,
                useSynchronizationContext: false));
            Input = _applicationPipe.Reader;
        }

        public void Start()
        {
            try
            {
                _sendTask = SendLoop();
                _receiveTask = ReceiveLoop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private async Task SendLoop()
        {
            try
            {
                while (true)
                {
                    // Read from the pipe
                    var readResult = await _transportPipe.Reader.ReadAsync();
                    var buffer = readResult.Buffer;

                    if (buffer.Length > 0) // Process non-empty buffers
                    {
                        _sender = _senderPool.Rent();

                        // Use sequence to extract data manually if necessary
                        foreach (var segment in buffer)
                        {
                            await _sender.SendAsync(_socket, segment.ToArray());
                        }

                        _senderPool.Return(_sender);
                        _sender = null;
                    }

                    // Advance the reader past the consumed data
                    _transportPipe.Reader.AdvanceTo(buffer.End);

                    // Break if the result signals completion or cancellation
                    if (readResult.IsCompleted || readResult.IsCanceled)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _applicationPipe.Writer.Complete();
                Shutdown();
            }
        }

        private async Task ReceiveLoop()
        {
            try
            {
                while (true)
                {
                    var buff = _applicationPipe.Writer.GetMemory(MinBuffSize);
                    var bytes = await _receiver.ReceiveAsync(_socket, buff);
                    if (bytes == 0)
                    {
                        break;
                    }
                    _applicationPipe.Writer.Advance(bytes);
                    var result = await _applicationPipe.Writer.FlushAsync();
                    if (result.IsCanceled || result.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _applicationPipe.Writer.Complete();
                Shutdown();
            }
        }

        public async ValueTask DisposeAsync()
        {
            _transportPipe.Reader.Complete();
            _applicationPipe.Writer.Complete();
            try
            {
                if (_receiveTask != null)
                {
                    await _receiveTask;
                }

                if (_sendTask != null)
                {
                    await _sendTask;
                }
            }
            finally
            {
                _receiver.Dispose();
                _sender?.Dispose();
            }
        }
        public void Shutdown()
        {
            lock (_shutdownLock)
            {
                if (_socketDisposed)
                {
                    return;
                }
                _socketDisposed = true;
                try
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }
                finally
                {
                    _socket.Dispose();
                }
            }
        }
    }
}

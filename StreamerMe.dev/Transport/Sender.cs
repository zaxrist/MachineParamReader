using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StreamerMe.dev.Transport
{
    public class Sender : AwaitableEventArgs
    {
        private short _token;
        private List<ArraySegment<byte>>? _buffers;

        public Task<int> SendAsync(Socket socket, byte[] data)
        {
            // Set buffer directly using the byte array
            SetBuffer(data, 0, data.Length);

            // If the send operation is asynchronous, handle it
            if (socket.SendAsync(this))
            {
                var tcs = new TaskCompletionSource<int>(_token++);
                Completed += (s, e) =>
                {
                    if (SocketError == SocketError.Success)
                    {
                        tcs.TrySetResult(BytesTransferred);
                    }
                    else
                    {
                        tcs.TrySetException(new SocketException((int)SocketError));
                    }
                };
                return tcs.Task;
            }

            // Handle immediate completion
            var transferred = BytesTransferred;
            var err = SocketError;

            if (err == SocketError.Success)
            {
                return Task.FromResult(transferred);
            }
            else
            {
                return Task.FromException<int>(new SocketException((int)err));
            }
        }

        public Task<int> SendAsync(Socket socket, IEnumerable<ArraySegment<byte>> data)
        {
            _buffers ??= new List<ArraySegment<byte>>();

            foreach (var segment in data)
            {
                _buffers.Add(segment);
            }

            BufferList = _buffers;

            if (socket.SendAsync(this))
            {
                var tcs = new TaskCompletionSource<int>(_token++);
                Completed += (s, e) =>
                {
                    if (SocketError == SocketError.Success)
                    {
                        tcs.TrySetResult(BytesTransferred);
                    }
                    else
                    {
                        tcs.TrySetException(new SocketException((int)SocketError));
                    }
                };
                return tcs.Task;
            }

            // Handle immediate completion
            var transferred = BytesTransferred;
            var err = SocketError;

            if (err == SocketError.Success)
            {
                return Task.FromResult(transferred);
            }
            else
            {
                return Task.FromException<int>(new SocketException((int)err));
            }
        }

        public void Reset()
        {
            if (BufferList != null)
            {
                BufferList = null;
                _buffers?.Clear();
            }
            else
            {
                SetBuffer(null, 0, 0);
            }
        }
    }
}

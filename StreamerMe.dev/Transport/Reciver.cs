using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StreamerMe.dev.Transport
{
    public class Receiver : AwaitableEventArgs
    {
        private short _token;

        public ValueTask<int> ReceiveAsync(Socket socket, Memory<byte> memory)
        {
            // Fix for SetBuffer: Properly setting the buffer using a compatible overload
            SetBuffer(memory.ToArray(), 0, memory.Length); // Converts Memory<byte> to byte[]

            // Start the async receive operation
            if (socket.ReceiveAsync(this))
            {
                return new ValueTask<int>(this, _token++);
            }

            // If no async operation is started, check for errors and return a completed ValueTask
            var transferred = BytesTransferred;
            var err = SocketError;

            if (err == SocketError.Success)
            {
                return new ValueTask<int>(transferred); // Return successful result
            }
            else
            {
                // Handle error by wrapping an exception in a Task and converting to ValueTask
                return new ValueTask<int>(Task.FromException<int>(new SocketException((int)err)));
            }
        }
    }
}


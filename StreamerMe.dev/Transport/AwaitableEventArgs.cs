using System;
using System.Net.Sockets;
using System.Threading.Tasks.Sources;

#nullable enable
namespace StreamerMe.dev.Transport
{
    public class AwaitableEventArgs : SocketAsyncEventArgs, IValueTaskSource<int>
    {
        private ManualResetValueTaskSourceCore<int> _source;

        public AwaitableEventArgs()
        {
            _source = new ManualResetValueTaskSourceCore<int>
            {
                // Ensure the proper ExecutionContext is ignored for better performance
                RunContinuationsAsynchronously = true
            };
        }

        protected override void OnCompleted(SocketAsyncEventArgs args)
        {
            if (SocketError != SocketError.Success)
            {
                _source.SetException(new SocketException((int)SocketError));
            }
            else
            {
                _source.SetResult(BytesTransferred);
            }
        }

        public int GetResult(short token)
        {
            // Returns the result and resets the source for reuse
            return _source.GetResult(token);
        }

        public ValueTaskSourceStatus GetStatus(short token)
        {
            // Check the current status of the task source
            return _source.GetStatus(token);
        }

        public void Reset()
        {
            // Resets the task source core for reuse
            _source.Reset();
        }

        public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            // Registers a continuation callback for the task
            _source.OnCompleted(continuation, state, token, flags);
        }
    }
}

﻿using StreamerMe.dev.Transport;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace StreamerMe.dev.Transport
{
    //From: https://github.com/dotnet/aspnetcore/blob/main/src/Servers/Kestrel/Transport.Sockets/src/Internal/SocketSenderPool.cs
    public class SenderPool : IDisposable
    {
        private readonly int MaxNumberOfSenders;
        private int _count;
        private readonly ConcurrentQueue<Sender> _senders = new ConcurrentQueue<Sender> ();
        private bool _disposed = false;

        public SenderPool(int maxNumberOfSenders = 128)
        {
            MaxNumberOfSenders = maxNumberOfSenders;
        }

        public Sender Rent()
        {
            if (_senders.TryDequeue(out var sender))
            {
                Interlocked.Decrement(ref _count);
                return sender;
            }

            return new Sender();
        }

        public void Return(Sender sender)
        {
            if (_disposed || _count >= MaxNumberOfSenders)
            {
                sender.Dispose();
            }
            else
            {
                Interlocked.Increment(ref _count);
                sender.Reset();
                _senders.Enqueue(sender);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            while (_senders.TryDequeue(out var sender))
            {
                sender.Dispose();
            }
        }
    }
}

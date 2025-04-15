

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace StreamerMe.dev.MemoryPool
{
    //From: https://github.com/dotnet/aspnetcore/blob/main/src/Shared/Buffers.MemoryPool/MemoryPoolBlock.cs
    /// <summary>
    /// Wraps an array allocated in the pinned object heap in a reusable block of managed memory
    /// </summary>
    internal sealed class MemoryPoolBlock : IMemoryOwner<byte>
    {
        internal MemoryPoolBlock(PinnedBlockMemoryPool pool, int length)
        {
            // Pool = pool; //This is for .NET5.0 or Newer

            //var pinnedArray = GC.AllocateUninitializedArray<byte>(length, pinned: true);

            // Memory = MemoryMarshal.CreateFromPinnedArray(pinnedArray, 0, pinnedArray.Length);

            // var array = new byte[length];
            // var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            // try
            // {
            //     IntPtr pointer = handle.AddrOfPinnedObject();
            //     // Use the pinned array
            // }
            // finally
            // {
            //     handle.Free();
            // }

            Pool = pool;

            // Replace the GC.AllocateUninitializedArray call with manual pinning
            var array = new byte[length]; // Allocate the array
            var handle = GCHandle.Alloc(array, GCHandleType.Pinned); // Pin the array

            try
            {
                // Use the pinned array as needed
                Memory = MemoryMarshal.CreateFromPinnedArray(array, 0, array.Length);
            }
            finally
            {
                handle.Free(); // Ensure the handle is freed to prevent memory leaks
            }

        }



        /// <summary>
        /// Back-reference to the memory pool which this block was allocated from. It may only be returned to this pool.
        /// </summary>
        public PinnedBlockMemoryPool Pool { get; }

        public Memory<byte> Memory { get; }

        public void Dispose()
        {
            Pool.Return(this);
        }
    }
}
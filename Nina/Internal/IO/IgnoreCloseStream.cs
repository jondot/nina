#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System;
using System.IO;
using System.Runtime.Remoting;

namespace Nina.Internal.IO
{
    internal class IgnoreCloseStream : Stream
    {
        bool _isClosed;
        readonly Stream _inner;
        
        public Stream Inner
        {
            get { return _inner; }
        }

        public override bool CanRead
        {
            get { return _isClosed ? false : _inner.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _isClosed ? false : _inner.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _isClosed ? false : _inner.CanWrite; }
        }

        public override long Length { get { NotClosedGuard(); return _inner.Length; } }

        public override long Position 
        { 
            get { NotClosedGuard(); return _inner.Position; } 
            set { NotClosedGuard(); _inner.Position = value; }
        }

        public IgnoreCloseStream(Stream inner)
		{
    		_inner = inner;
		}

		void NotClosedGuard()
		{
			if(_isClosed) throw new InvalidOperationException("The stream have been closed");
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, 
			                                   AsyncCallback callback, object state)
		{
			NotClosedGuard();
			return _inner.BeginRead(buffer, offset, count, callback, state);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, 
			                                    AsyncCallback callback, object state)
		{
			NotClosedGuard();
			return _inner.BeginWrite(buffer, offset, count, callback, state);
		}

		public override void Close()
		{
			if(!_isClosed) _inner.Flush();
			_isClosed = true;			
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			NotClosedGuard();
			return _inner.EndRead(asyncResult);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			NotClosedGuard();
			_inner.EndWrite(asyncResult);
		}

		public override void Flush()
		{
			NotClosedGuard();
			_inner.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			NotClosedGuard();
			return _inner.Read(buffer, offset, count);
		}

		public override int ReadByte()
		{
			NotClosedGuard();
			return _inner.ReadByte();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			NotClosedGuard();
			return _inner.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			NotClosedGuard();
			_inner.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			NotClosedGuard();
			_inner.Write(buffer, offset, count);
		}

		public override void WriteByte(byte value)
		{
			NotClosedGuard();
			_inner.WriteByte(value);
		}
        
        public override ObjRef CreateObjRef(Type requestedType)
        {
            throw new NotSupportedException();
        }

        public override object InitializeLifetimeService()
        {
            throw new NotSupportedException();
        }
    }
}

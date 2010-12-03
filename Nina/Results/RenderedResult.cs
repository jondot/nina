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
using System.Web;
using Nina.Support;

namespace Nina.Results
{
    internal abstract class RenderedResult<T>:ResourceResult
    {
        protected T _result;
        protected string _contentType="text/html";
        private MemoryStream _cachedSerializedResult;
        protected abstract void Render(Stream s, T t);

        public override void Execute(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = _contentType;
            if(_cachedSerializedResult != null)
            {
                _cachedSerializedResult.WriteTo(response.OutputStream);
                _cachedSerializedResult.Dispose();
            }
            else
            {
                Render(response.OutputStream,_result);
            }
        }

        public override string GetResultETag(Func<Stream, string> digest)
        {
            var m = new MemoryStream();
            Render(m, _result);
            _cachedSerializedResult = m;
            if(m.CanSeek)
            {
                m.Seek(0, SeekOrigin.Begin);
            }
            return digest(m);
        }

        public override string GetResultETag()
        {
            return GetResultETag(Digest.CRC32);
        }
    }
}
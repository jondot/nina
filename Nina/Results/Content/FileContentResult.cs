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

namespace Nina.Results.Content
{
    internal class FileContentResult : ResourceResult
    {
        private readonly string _contentType;
        private readonly byte[] _content;
        public FileContentResult(byte[] content, string contenttype)
        {
            _content = content;
            _contentType = contenttype;
        }
        public override void Execute(HttpContext context)
        {
            context.Response.ContentType = _contentType;
            context.Response.OutputStream.Write(_content, 0, _content.Length);
        }

        public override string GetResultETag(Func<Stream, string> digest)
        {
            using(var m = new MemoryStream(_content))
            {
                return digest(m);
            }
        }

        public override string GetResultETag()
        {
            return GetResultETag(Digest.CRC32);
        }
    }
}
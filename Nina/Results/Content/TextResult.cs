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
using System.Text;
using System.Web;
using Nina.Support;

namespace Nina.Results.Content
{
    public class TextResult : ResourceResult
    {
        private readonly string _text;

        public TextResult(string text)
        {
            _text = text;
        }

        public override void Execute(HttpContext context)
        {
            context.Response.Write(_text);
        }

        public override string GetResultETag(Func<Stream, string> digest)
        {
            using (var m = new MemoryStream(Encoding.Default.GetBytes(_text)))
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
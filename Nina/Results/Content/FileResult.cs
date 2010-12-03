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

namespace Nina.Results.Content
{
    internal class FileResult : ResourceResult
    {
        private readonly string _contentType;
        private readonly string _path;
        public FileResult(string path, string contenttype)
        {
            _path = path;
            _contentType = contenttype;
        }
        public override void Execute(HttpContext context)
        {
            context.Response.ContentType = _contentType;
            context.Response.TransmitFile(_path);
        }

        public override string GetResultETag(Func<Stream, string> digest)
        {
            using(var fileStream = File.OpenRead(_path))
            {
                return digest(fileStream);
            }
        }

        public override string GetResultETag()
        {
            return File.GetLastWriteTime(_path).Ticks.ToString();
        }
    }
}
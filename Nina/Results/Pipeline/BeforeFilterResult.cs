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

namespace Nina.Results.Pipeline
{
    public abstract class BeforeFilterResult : ResourceResult
    {
        protected readonly ResourceResult _inner;

        protected BeforeFilterResult(ResourceResult inner)
        {
            _inner = inner;
        }
        public override void Execute(HttpContext context)
        {
            Filter(context);
            _inner.Execute(context);
        }
        public override string GetResultETag(Func<Stream, string> digest)
        {
            return _inner.GetResultETag(digest);
        }
        public override string GetResultETag()
        {
            return _inner.GetResultETag();
        }
        protected abstract bool Filter(HttpContext context);
    }
}
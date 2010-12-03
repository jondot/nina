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

namespace Nina.Results.Meta
{
    public abstract class NonETaggedResult : ResourceResult
    {
        public override string GetResultETag(Func<Stream, string> digest)
        {
            return string.Empty;
        }

        public override string GetResultETag()
        {
            return string.Empty;
        }
    }
}
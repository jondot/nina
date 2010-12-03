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
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Nina.Results.Http;
using Nina.Results.Meta;

namespace Nina.Results
{
    
    public abstract class ResourceResult
    {
        protected NameValueCollection ResultHeaders;
        public abstract void Execute(HttpContext context);
        public abstract string GetResultETag(Func<Stream, string> digest);
        public abstract string GetResultETag();

        public ResourceResult Created()
        {
            return new HttpCreatedResult(string.Empty, this);
        }

        public ResourceResult Created(string uri)
        {
            return new HttpCreatedResult(uri, this);
        }

        public ResourceResult ETagged(Func<Stream, string> digest)
        {
            return new ETaggedResult(GetResultETag(digest), this);
        }


        public ResourceResult ETagged(string etag)
        {
            return new ETaggedResult(etag, this);
        }

        public ResourceResult ETagged()
        {
            return new ETaggedResult(GetResultETag(), this);
        }

    }

}
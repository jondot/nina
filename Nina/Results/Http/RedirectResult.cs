#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Web;
using Nina.Results.Meta;

namespace Nina.Results.Http
{
    internal class RedirectResult : NonETaggedResult
    {
        private readonly string _url;

        public RedirectResult(string url)
        {
            _url = url;
        }
        public override void Execute(HttpContext context)
        {
            context.Response.Redirect(_url, true);
        }
    }
}
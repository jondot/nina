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
    public class StatusResult : NonETaggedResult
    {
        private readonly int _statusCode;
        private readonly string _body;

        public StatusResult(int statusCode, string body)
        {
            _statusCode = statusCode;
            _body = body;
        }

        public override void Execute(HttpContext context)
        {
            if(!string.IsNullOrEmpty(_body)) context.Response.Write(_body);
            context.Response.StatusCode = _statusCode;
        }
    }
}
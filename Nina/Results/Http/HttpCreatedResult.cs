#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Net;
using System.Web;
using Nina.Results.Pipeline;

namespace Nina.Results.Http
{
    public class HttpCreatedResult : BeforeFilterResult
    {
        private readonly string _location;

        public HttpCreatedResult(string location, ResourceResult inner) : base(inner)
        {
            _location = location;
        }

        protected override bool Filter(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Created;
            context.Response.Headers.Add("Location",
                                         _location == string.Empty ? context.Request.Url.ToString() : _location);
            return true;
        }
    }
}
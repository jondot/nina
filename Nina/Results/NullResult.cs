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

namespace Nina.Results
{
    internal class NullResult : NonETaggedResult
    {
        public override void Execute(HttpContext context)
        {
        }
    }
}
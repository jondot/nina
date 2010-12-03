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
using System.Web;
using Nina.Filters.Meta;

namespace Nina.Support
{
    /// <summary>
    /// An out-of-band filters helper class for flow control.
    /// Use this when *you* want to determine where are flow control points of your data.
    /// 
    /// Those filters are capable of halting the response.
    /// </summary>
    public static class Filters
    {
        public static bool ETag(string value)
        {
            return new ETagFilter(value).Execute(HttpContext.Current);
        }

        public static bool LastModified(DateTime value)
        {
            return new LastModifiedFilter(value).Execute(HttpContext.Current);
        }
    }
}

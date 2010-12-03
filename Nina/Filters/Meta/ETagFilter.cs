#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Linq;
using System.Web;

namespace Nina.Filters.Meta
{
    public class ETagFilter : IFilter
    {
        private string _value;
        private readonly IFilter _beforeFilter;

        public ETagFilter(string value)
        {
            _value = value;
        }

        public ETagFilter(string value, IFilter filter) :this(value)
        {
            _beforeFilter = filter;
        }

        public bool Execute(HttpContext c)
        {
            if (_beforeFilter != null && !_beforeFilter.Execute(c))
                return false;

            _value = string.Format("\"{0}\"", _value);
            c.Response.AppendHeader("ETag", _value);
            var etagString = c.Request.Headers["if-none-match"];

            // if-none-match behavior
            if (!string.IsNullOrEmpty(etagString))
            {
                var etags = etagString.Split(',');
                if (etags.Any(x => x.Trim().Equals(_value)))
                {
                    c.Response.StatusCode = 304;
                    c.Response.SuppressContent = true;
                    c.Response.ClearContent();
                    c.ApplicationInstance.CompleteRequest();
                    
                    return false;
                }
            }
            return true;
        }
    }
}
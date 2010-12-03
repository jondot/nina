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

namespace Nina.Filters.Meta
{
    public class LastModifiedFilter : IFilter
    {
        private DateTime _value;

        public LastModifiedFilter(DateTime value)
        {
            _value = value;
        }

        public bool Execute(HttpContext c)
        {
            c.Response.AppendHeader("Last-Modified", _value.ToString("r"));

            var modString = c.Request.Headers["If-Modified-Since"];
            if(!string.IsNullOrEmpty(modString))
            {
                DateTime ifModified;
                if(DateTime.TryParse(modString, out ifModified))
                {
                    if(_value - ifModified > TimeSpan.FromSeconds(1))
                    {
                        c.Response.StatusCode = 304;
                        c.Response.SuppressContent = true;
                        c.Response.ClearContent();
                        c.ApplicationInstance.CompleteRequest();

                        //HttpContext.Current.Response.End();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
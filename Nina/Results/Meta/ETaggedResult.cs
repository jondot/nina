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
using Nina.Filters.Meta;
using Nina.Results.Pipeline;

namespace Nina.Results.Meta
{
    public class ETaggedResult : BeforeFilterResult
    {
        private readonly string _value;

        public ETaggedResult(string value, ResourceResult inner) : base(inner)
        {
            _value = value;
        }

        protected override bool Filter(HttpContext context)
        {
            if(!string.IsNullOrEmpty(_value))
            {
                return new ETagFilter(_value).Execute(context);
            }
            
            return true;
        }
    }
}
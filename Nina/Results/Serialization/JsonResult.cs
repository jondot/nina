#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nina.Results.Serialization
{
    internal class JsonResult : RenderedResult<object>
    {
        private static readonly IsoDateTimeConverter _isoDateTimeConverter = new IsoDateTimeConverter();

        public JsonResult(object result, string contentType) : this(result)
        {
            _contentType = contentType;
        }
        public JsonResult(object result)
        {
            _contentType = "application/json";
            _result = result;
        }

        protected override void Render(Stream s, object t)
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_result, _isoDateTimeConverter));
            s.Write(buffer,0,buffer.Length);
        }
    }
}
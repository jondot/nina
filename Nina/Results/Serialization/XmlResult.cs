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
using System.Xml.Serialization;

namespace Nina.Results.Serialization
{
    internal class XmlResult<T> : RenderedResult<T>
    {
        // using T for static type caching.
        private static readonly XmlSerializer _serializer= new XmlSerializer(typeof(T));

        public XmlResult(T result, string contentType) : this(result)
        {
            _contentType = contentType;
        }
        public XmlResult(T result)
        {
            _contentType = "application/xml";
            _result = result;
        }

        protected override void Render(Stream s, T result)
        {
            _serializer.Serialize(s, _result);
        }
    }

}
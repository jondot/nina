#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Collections.Generic;
using System.IO;
using Nina.Internal.IO;
using Nina.ViewEngines;

namespace Nina.Results.ViewEngines
{
    
    internal class ViewResult<T> : RenderedResult<T>
    {
        private readonly T _data;
        private readonly string _templateName;
        private static readonly Dictionary<string, ITemplate> _temps = new Dictionary<string, ITemplate>();

        public ViewResult(string templateName, T data)
        {
            _templateName = templateName;
            _data = data;
        }

        protected override void Render(Stream s, T t)
        {
            ITemplate templ;
    
            if (Configuration.Configure.IsDevelopment || !_temps.TryGetValue(_templateName, out templ))
            {
                templ = Configuration.Configure.Views.Engine.Compile<T>(_templateName);
                _temps[_templateName] = templ;
            }
            using(var sw = new StreamWriter(new IgnoreCloseStream(s)))
            {
                templ.Render(sw, _data);
            }
        }
    }
}
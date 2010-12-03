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
using NHaml;

namespace Nina.ViewEngines.NHaml
{
    internal class NHamlCompiledTemlpate : ITemplate
    {
        private readonly CompiledTemplate _template;

        public NHamlCompiledTemlpate(CompiledTemplate compiledTemplate)
        {
            _template = compiledTemplate;
        }

        public void Render<T>(TextWriter output, T data)
        {
            var dataView = (DataView<T>)_template.CreateInstance();
            dataView.ViewData = data;
            dataView.Render(output);
        }
    }
}
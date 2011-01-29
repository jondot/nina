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
using System.Collections.Generic;
using System.IO;
using System.Web;
using NHaml;

namespace Nina.ViewEngines.NHaml
{
    internal class NHamlTemplateEngine : ITemplateEngine
    {
        private TemplateEngine _engine = new TemplateEngine();

        public ITemplate Compile<T>(string template)
        {
            return Compile(typeof(T), template);
        }

        public ITemplate Compile(Type modelType, string template)
        {
            if(Configuration.Configure.IsDevelopment)
            {
                _engine = new TemplateEngine();
            }

            Type dataView = typeof(DataView<>);
            Type templateType = dataView.MakeGenericType(new[] { modelType });
            CompiledTemplate ct;
            if(!string.IsNullOrEmpty(Configuration.Configure.Views.Layout))
            {
                ct = _engine.Compile(new List<string>
                {
                    Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Configuration.Configure.Views.Layout),
                    Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, template)
                }, templateType);
            }
            else
            {
                ct = _engine.Compile(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, template), templateType);
            }

            return new NHamlCompiledTemlpate(ct);
        }
    }
}

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
using NDjango;
using NDjango.Interfaces;

namespace Nina.ViewEngines.NDjango
{
    public class DjangoEngine : ITemplateEngine
    {
        private static TemplateManagerProvider provider;
        private static ITemplateManager manager;
        public const string EXT = "dj";

        static DjangoEngine()
        {
            provider = new TemplateManagerProvider().WithLoader(new IntegrationTemplateLoader());
            manager = provider.GetNewManager();
        }

        public ITemplate Compile<T>(string template)
        {

            return new NDjangoTemplate(template, manager);
        }
    }

    internal  class NDjangoTemplate : ITemplate
    {
        private string _template;
        private ITemplateManager _manager;
        private Dictionary<string, object> _dict;

        public NDjangoTemplate(string template, ITemplateManager manager)
        {
            _manager = manager;
            _template = template;
            _dict = new Dictionary<string, object>();
        }

        public void Render<T>(TextWriter output, T data)
        {
            _dict.Clear();
            _dict["ViewData"] = data;
            var reader = _manager.RenderTemplate(_template, _dict);
            char[] buffer = new char[4096];
            int count = 0;
            while ((count = reader.ReadBlock(buffer, 0, 4096)) > 0)
                output.Write(buffer, 0, count);
        }
    }

    internal class IntegrationTemplateLoader : ITemplateLoader
    {
        public TextReader GetTemplate(string path)
        {
            return File.OpenText(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, path+"."+DjangoEngine.EXT));
        }

        public bool IsUpdated(string path, DateTime timestamp)
        {
            return false;
        }
    }
}

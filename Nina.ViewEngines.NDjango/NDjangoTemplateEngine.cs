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

        public ITemplate Compile(Type modelType, string template)
        {
            return new NDjangoTemplate(template, manager);
        }
    }
}

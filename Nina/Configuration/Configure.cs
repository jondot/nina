#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using Nina.ViewEngines;

namespace Nina.Configuration
{
    public class Configure
    {
        private static readonly ViewConfiguration _views = new ViewConfiguration();
        public static ViewConfiguration Views { get { return _views; } }
        public static bool IsDevelopment { get; set; }
    }

    public class ViewConfiguration
    {
        public string ViewEngineExtension { get; set; }
        public ITemplateEngine Engine { get; set; }
        public string Layout { get; set; }
    }
}

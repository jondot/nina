#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using Nina.ViewEngines.NDjango;

namespace Nina.Configuration
{
    public static class NDjangoConfig
    {
        public static void WithNDjango(this ViewConfiguration config)
        {
            config.ViewEngineExtension = "dj";
            config.Engine = new DjangoEngine();
        }
    }
}

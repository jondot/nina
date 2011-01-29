﻿#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
namespace Nina.ViewEngines
{
    public interface ITemplateEngine
    {
        ITemplate Compile<T>(string template);
    }
}

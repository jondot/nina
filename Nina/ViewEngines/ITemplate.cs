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

namespace Nina.ViewEngines
{
    public interface ITemplate
    {
        void Render<T>(TextWriter output, T data);
    }
}

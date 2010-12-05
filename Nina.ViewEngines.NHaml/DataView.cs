#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using NHaml;

namespace Nina.ViewEngines.NHaml
{
    public class DataView<T> : Template
    {
        public T ViewData { get; set; }
        public T Model { get { return ViewData; } }
    }
}

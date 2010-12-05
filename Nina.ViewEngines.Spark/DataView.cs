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
using Spark;

namespace Nina.ViewEngines.Spark
{
    public abstract class DataView<T> : AbstractSparkView
    {
        public T ViewData { get; set; }
        public T Model { get { return ViewData; } }
        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override Guid GeneratedViewId
        {
            get { throw new NotImplementedException(); }
        }
    }
    
}

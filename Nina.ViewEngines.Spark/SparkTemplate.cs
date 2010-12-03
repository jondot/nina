#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using Spark;

namespace Nina.ViewEngines.Spark
{
    internal class SparkTemplate : ITemplate
    {
        private readonly AbstractSparkView _sparkView;

        public SparkTemplate(AbstractSparkView sparkView)
        {
            _sparkView = sparkView;
        }


        public void Render<T>(System.IO.TextWriter output, T data)
        {
            var view = (DataView<T>) _sparkView;
            view.ViewData = data;
            view.RenderView(output);
        }

    }
}
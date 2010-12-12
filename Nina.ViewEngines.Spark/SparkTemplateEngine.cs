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
using System.IO;
using System.Web;
using Spark;
using Spark.FileSystem;
using System.Linq;

namespace Nina.ViewEngines.Spark
{
    internal class SparkTemplateEngine : ITemplateEngine
    {
        public ITemplate Compile<T>(string template)
        {
            SparkViewEngine _engine = new SparkViewEngine { ViewFolder = new FileSystemViewFolder(HttpContext.Current.Request.PhysicalApplicationPath), DefaultPageBaseType = string.Format("Nina.ViewEngines.Spark.DataView<{0}>", typeof(T).FullName) };
            
            SparkViewDescriptor vds = new SparkViewDescriptor();

            
            vds.AddTemplate(template+".spark");
            var layout = DetectLayout(template, _engine.ViewFolder);
            if(!string.IsNullOrEmpty(layout))
            {
                vds.AddTemplate(layout);
            }
            var sparkView = (DataView<T>)_engine.CreateInstance(vds);
            return new SparkTemplate(sparkView);
        }

        private static string DetectLayout(string template, IViewFolder viewFolder)
        {
            var possibleLayouts = new[]
                                      {
                                          Configuration.Configure.Views.Layout??string.Empty,
                                          "layouts/application.spark",
                                          "views/layouts/application.spark",
                                          "views/application.spark",
                                          "views/application.spark",
                                          Path.Combine(Path.GetDirectoryName(template), "layouts/application.spark"),
                                          Path.Combine(Path.GetDirectoryName(template), "application.spark")
                                      };
            return possibleLayouts.First(x => viewFolder.HasView(x));
        }
    }
}

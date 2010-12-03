#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Web;
using Spark;
using Spark.FileSystem;

namespace Nina.ViewEngines.Spark
{
    internal class SparkTemplateEngine : ITemplateEngine
    {
        public ITemplate Compile<T>(string template)
        {
            ISparkViewEngine _engine = new SparkViewEngine { ViewFolder = new FileSystemViewFolder(HttpContext.Current.Request.PhysicalApplicationPath), DefaultPageBaseType = string.Format("Nina.ViewEngines.Spark.DataView<{0}>", typeof(T).FullName) };
            
            SparkViewDescriptor vds = new SparkViewDescriptor();
            vds.AddTemplate(template+".spark");
            var sparkView = (DataView<T>)_engine.CreateInstance(vds);
            return new SparkTemplate(sparkView);
        }
    }
}

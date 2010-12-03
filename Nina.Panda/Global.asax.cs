#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Web.Routing;
using Nina.Configuration;
using Nina.Routing;


namespace Nina.Panda
{
    public class NinaHttpApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Configure.Views.WithNHaml();
            Configure.Views.Layout = "views/application";
            Configure.IsDevelopment = false;
            // optimization - this causes 2 filesystem checks to happen when false.
            RouteTable.Routes.RouteExistingFiles = false;
            
            routes.Add(new MountingPoint<Panda>("/"));
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
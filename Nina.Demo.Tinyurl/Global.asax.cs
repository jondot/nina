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
using Nina.Routing;

namespace Nina.Demo.Tinyurl
{
    
    public class NinaHttpApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("{resource}.axd/{*pathInfo}", new StopRoutingHandler()));
            routes.Add(new Route("favicon.ico", new StopRoutingHandler()));

            routes.Add(new MountingPoint<TinyUrl>("/", () => new TinyUrl(new Urls())));
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
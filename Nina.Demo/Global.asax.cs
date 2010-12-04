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
using System.Web.Routing;
using AddressBook.Contacts;
using Nina.Configuration;
using Nina.Routing;

namespace Nina.Demo
{
	public class Global : System.Web.HttpApplication
	{

		private static void RegisterRoutes()
		{
            
		    Configure.Views.WithNHaml();
            // optimization - this causes 2 filesystem checks to happen when false.
		    RouteTable.Routes.RouteExistingFiles = true;
            
            RouteTable.Routes.Add(new MountingPoint<Contacts>("contacts"));
            RouteTable.Routes.Add(new MountingPoint<Posts>("blog"));
		}


		protected void Application_Start(object sender, EventArgs e)
		{
			RegisterRoutes();
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}
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
using Nina.ViewEngines.NHaml;
using Nina.ViewEngines.Spark;
using Nina.ViewEngines.NDjango;

namespace Nina.AddressBook.Contacts
{
	public class Global : System.Web.HttpApplication
	{

		private static void RegisterRoutes()
		{
            Configure.Views.WithNDjango();
            
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

		/*
		protected void Application_Error(object sender, EventArgs e)
		{
			// Get the unhandled exception
			Exception ex = Server.GetLastError();

			// Potentially do some logging with the ex object, possibly using Elmah (http://code.google.com/p/elmah/) or some other library
			// ...

			if (ex is RestException)
			{
				RestResponseHelper.WriteRestException((RestException)ex);
				
				// Clear the error (prevent the yellow screen of death)
				Server.ClearError();
			}
			else
			{
				// We have an exception, but it's not a RestException.
				// We could just let the yellow screen of death go down to the client by doing nothing and not clearing
				// the error, but since this application is ONLY for REST services (it doesn't host any content pages, etc),
				// I know that the request is an async request (xhr) from the client, so I might as well send down a RestException
				// anyway, and clear the error here.  A yellow screen of death isn't of much use as the response to an xhr call
				// from javascript.
				RestResponseHelper.WriteRestException(new RestException(ex.Message, ex));
				Server.ClearError();
			}
		}
		*/

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}
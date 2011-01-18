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
using System.Web;
using System.Web.Routing;

namespace Nina.Routing
{
	public class MountingPoint<T> : RouteBase where T : NinaBaseHandler
	{
		private readonly string _mountedUrl;
	    private readonly MountedRouteHandler<T> _factory;

        public MountingPoint(string mountedUrl) : this(mountedUrl, Activator.CreateInstance<T>)
        {
        }

        public MountingPoint(string mountedUrl, Func<T> applicationFactory)
	    {
	        if (applicationFactory == null)
	        {
	            throw new ArgumentNullException("applicationFactory");
	        }
            
            _mountedUrl = mountedUrl;

            if (mountedUrl.StartsWith("/"))
            {
                _mountedUrl = "~" + mountedUrl;
            }
            else if (!mountedUrl.StartsWith("~/"))
            {
                _mountedUrl = "~/" + mountedUrl;
            }

            _factory = new MountedRouteHandler<T>(this, _mountedUrl, applicationFactory);
		}

		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			
            if(!httpContext.Request.AppRelativeCurrentExecutionFilePath.StartsWith(_mountedUrl))
				return null;

            var rdata = new RouteData(this, _factory);

			return rdata;
		}

	    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			return null;
		}
	}
}
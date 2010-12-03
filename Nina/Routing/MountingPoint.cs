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
using System.Web.Routing;

namespace Nina.Routing
{
	public class MountingPoint<T> : RouteBase where T : NinaBaseHandler, new()
	{
		private readonly string _mountedUrl;
	    private MountedRouteHandler<T> _factory;

	    public MountingPoint(string mountedUrl)
	    {
	        _mountedUrl = mountedUrl;

            if (mountedUrl.StartsWith("/"))
            {
                _mountedUrl = "~" + mountedUrl;
            }
            else if (!mountedUrl.StartsWith("~/"))
            {
                _mountedUrl = "~/" + mountedUrl;
            }


            _factory = new MountedRouteHandler<T>(this, _mountedUrl);
		}

		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			
            if(!httpContext.Request.AppRelativeCurrentExecutionFilePath.StartsWith(_mountedUrl))
				return null;

            RouteData rdata = new RouteData(this, _factory);
			return rdata;
		}

	    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			return null;
		}
	}
}
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
	internal class MountedRouteHandler<T> : IRouteHandler where T : NinaBaseHandler, new()
	{
		private readonly RouteBase _route;
		private readonly string _mountingPoint;
	    private readonly T _httpHandler;
	    private readonly string _absolute;

	    public MountedRouteHandler(RouteBase route, string mountingPoint)
		{
			_route = route;
			_mountingPoint = mountingPoint;

            //optimization: we have the same handler instance for all requests
	        _absolute = VirtualPathUtility.ToAbsolute(_mountingPoint);
            
            _httpHandler = new T { Route = _route, MountingPointVirtual = _mountingPoint, MountingPointPath = _absolute };
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
		    return _httpHandler;// new T { Route = _route, MountingPointVirtual = _mountingPoint, MountingPointPath = _absolute };
		}
	}
}

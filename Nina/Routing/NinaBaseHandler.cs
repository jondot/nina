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
	public abstract class NinaBaseHandler : IHttpHandler
	{
		public RouteBase Route { get; internal set; }
		public string MountingPointVirtual { get; internal set; }
        public string MountingPointPath { get; internal set; }
		public abstract void ProcessRequest(HttpContext context);
		public abstract bool IsReusable { get; }
	}
}

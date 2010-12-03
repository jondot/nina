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


namespace Nina.Helpers
{
	public static class UrlHelpers
	{
		public static string ResolveUrl(string tildaUrl)
		{
			if (tildaUrl == null)
				return null;

            if (tildaUrl.StartsWith("~"))
                return VirtualPathUtility.ToAbsolute(tildaUrl);

			return tildaUrl;
		}

		public static string ResolveAbsoluteUrl(string tildaUrl)
		{
			if (tildaUrl.IndexOf("://") > -1)
				return tildaUrl;
            
            if(HttpContext.Current.Request.IsSecureConnection)
                return string.Format("https://{0}{1}", HttpContext.Current.Request.Url.Authority, VirtualPathUtility.ToAbsolute(tildaUrl));
            return string.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, VirtualPathUtility.ToAbsolute(tildaUrl));
		}
	}
}

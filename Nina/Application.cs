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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Nina.Results;
using Nina.Results.Content;
using Nina.Results.Http;
using Nina.Results.Serialization;
using Nina.Results.ViewEngines;
using Nina.Routing;
using Nina.StringResources;


namespace Nina
{
	public abstract class Application : NinaBaseHandler
	{
        readonly Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>> _puts = new Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>>();
        readonly Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>> _deletes = new Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>>();
        readonly Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>> _posts = new Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>>();
        readonly Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>> _gets = new Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>>();
        private Func<HttpContext, ResourceResult> _notFound;
        private static readonly byte[] _nina404Png;

		public override bool IsReusable
		{
			get { return true; }
		}

	   

	    //
        // initialize domain-wide heavy duty resources
        //
        static Application()
        {
            using(var m = new MemoryStream())
            {
                Resources.pole.Save(m, ImageFormat.Png);
                _nina404Png = m.GetBuffer();
            }
        }

	    protected Application()
        {
            _notFound = NotFoundInternal;
            Get("nina404.png", (m, c) => File(_nina404Png, "image/png"));
        }

	    public override void ProcessRequest(HttpContext context)
		{
            Dictionary<UriTemplate, Func<NameValueCollection, HttpContext, ResourceResult>> methods;
			string verb = context.Request.HttpMethod.ToUpper();
			switch (verb)
			{
				case "GET":
			        methods = _gets;
					break;

				case "PUT":
			        methods = _puts;
					break;

				case "POST":
			        methods = _posts;
			        var s = context.Request.Form["_method"];
			        if(!string.IsNullOrEmpty(s))
			        {
			            switch (s.ToUpper())
			            {
			                case "PUT":
			                    methods = _puts;
			                    break;
			                case "DELETE":
			                    methods = _deletes;
			                    break;
			            }
			        }
					break;

				case "DELETE":
			        methods = _deletes;
					break;

				default:
					throw new ApplicationException(string.Format("Non-supported HTTP verb:{0}",verb));
			}

	        var baseUri = CreateBaseUrl(context.Request.Url, MountingPointPath);
			UriTemplateMatch match;
			foreach (var m in methods)
			{
				match = m.Key.Match(baseUri, context.Request.Url);
				if (match != null)
				{
				    var blockResult = m.Value(match.BoundVariables, context);
                    if (context.Response.SuppressContent) return;
				    if(blockResult != null) blockResult.Execute(context);
				    return;
				}
			}


            _notFound(context).Execute(context);
		}

	    private static Uri CreateBaseUrl(Uri url, string mountingPointPath)
	    {
            if (HttpContext.Current.Request.IsSecureConnection)
                return new Uri(string.Format("https://{0}{1}", url.Authority, VirtualPathUtility.ToAbsolute(mountingPointPath)));
            return new Uri(string.Format("http://{0}{1}", url.Authority, VirtualPathUtility.ToAbsolute(mountingPointPath)));
	    }


	    //
        // builders
        //
        protected void Get(string action, Func<NameValueCollection, HttpContext, ResourceResult> block)
	    {
	        _gets[new UriTemplate(action)] = block;
	    }

        protected void Post(string action, Func<NameValueCollection, HttpContext, ResourceResult> block)
        {
            _posts[new UriTemplate(action)] = block;
        }

        protected void Put(string action, Func<NameValueCollection, HttpContext, ResourceResult> block)
        {
            _puts[new UriTemplate(action)] = block;
        }

        protected void Delete(string action, Func<NameValueCollection, HttpContext, ResourceResult> block)
        {
            _deletes[new UriTemplate(action)] = block;
        }

	    protected void NotFound(Func<HttpContext, ResourceResult> action)
	    {
	        _notFound = action;
	    }

        private ResourceResult NotFoundInternal(HttpContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.Write(Strings.HTML_404.Replace("{NINA404}", MountingPointPath));
            return Nothing();
        }



        //
        // renderers
        //
	    protected ResourceResult View<T>(string foo, T viewdata)
	    {
            return new ViewResult<T>(foo, viewdata);
	    }



        //
        // Results
        //
        protected ResourceResult Error(string body)
        {
            return Status(500, body);
        }

        protected ResourceResult Error()
        {
            return Status(500);
        }

        protected ResourceResult Status(int statusCode)
        {
            return new StatusResult(statusCode, string.Empty);
        }

        protected ResourceResult Status(int statusCode, string body)
        {
            return new StatusResult(statusCode, body);
        }

        protected ResourceResult Nothing()
        {
            return new NullResult();
        }

        protected ResourceResult File(byte [] content, string contenttype)
        {
            return new FileContentResult(content, contenttype);
            
        }

        protected ResourceResult File(string path, string contenttype)
        {
            return new FileResult(path, contenttype);
        }

	    protected ResourceResult Redirect(string url)
	    {
	        return new RedirectResult(url);
	    }

        protected ResourceResult Text(string text)
        {
            return new TextResult(text);
        }

        protected ResourceResult Json(object model)
        {
            return new JsonResult(model);
        }

        protected ResourceResult Json(object model, string contentType)
        {
            return new JsonResult(model, contentType);
        }

        protected ResourceResult Xml<T>(T model)
        {
            return new XmlResult<T>(model);
        }

        protected ResourceResult Xml<T>(T model, string contentType)
        {
            return new XmlResult<T>(model, contentType);
        }
	}
}

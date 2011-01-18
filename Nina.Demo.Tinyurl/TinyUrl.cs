#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Collections.Generic;

namespace Nina.Demo.Tinyurl
{
    public class TinyUrl : Nina.Application
    {
        public TinyUrl(Urls urls)
        {
            Get("/", (m,c) => Text("<html><body>Tiny url!<form method='post'><input type='text' name='url'/><input type='submit' value='tiny!'/></form></body></html>"));
            
            Post("/", (m,c)=>
            {
                var url = urls.Save(c.Request.Form["url"]);
                return Text(string.Format("<html><body>Your url: <a href='{0}'>{0}</a></body></html>", c.Request.Url +url));
            });

            Get("/{tinyurl}", (m, c) => Redirect(urls.Get(m["tinyurl"])) );
        }
    }

    public class Urls
    {
        private readonly List<string> _urls = new List<string>();

        public string Save(string s)
        {
            _urls.Add(s);
            return (_urls.Count-1).ToString();
        }
        public string Get(string u)
        {
            var i = int.Parse(u);
            return _urls[i];
        }
    }
}
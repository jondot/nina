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
using Newtonsoft.Json;
using Nina.Support;


namespace AddressBook.Contacts
{
    
	public class Contacts : Nina.Application
	{
        
        public Contacts()
        {
            Get("book.xml", (m, c) =>
            {
                Filters.ETag("8");
                return Xml(new Model());
                //return Redirect("book/hello");
            });

            Get("etagged/crc/book.xml", (m, c) =>
            {
                return Xml(new Model()).ETagged();
                //return Redirect("book/hello");
            });

            //-n 1000 http://localhost:8080/va2/contacts/etagged/md5/book.xml
            //Requests per second:    790.12 [#/sec] (mean)
            Get("etagged/md5/book.xml", (m, c) =>
            {
                return Xml(new Model()).ETagged(Digest.MD5);
                //return Redirect("book/hello");
            });

            //ab -n 1000 http://localhost:8080/va2/contacts/book.json
            //Requests per second:    888.62 [#/sec] (mean)
            Get("book.json", (m, c) =>
            {
                return Json(new Model(), "text/html");
            });
            //ab -n 1000 http://localhost:8080/va2/contacts/book.json.htc
            //Requests per second:    888.62 [#/sec] (mean)
            Get("book.json.htc", (m, c) =>
            {
                c.Response.Write(JsonConvert.SerializeObject(new Model()));
                return Nothing();
            });

            
            
            Get("book", (m, c) => Text("book"));

            
            Get("book.htc", (m, c) =>
                                {
                                    c.Response.Write("book");
                                    return Nothing();
                                });

            Get("book/{name}", (m, c) =>
            {
                UriTemplate t = new UriTemplate("");
                
                //c.Response.Write(string.Format("hello {0}",m["name"]));
                return View("views/foo", new Model()).Created(c.Request.Url.ToString());
            });

            Get("spark", (m, c) =>
            {
                //c.Response.Write(string.Format("hello {0}",m["name"]));
                return View("views/foo", new Model());
            });

            Post("book", (m, c) =>
            {
                var s = c.Request.Form["book_name"];
                return Text("thanks. i posted"+s).ETagged();
            });

            Put("book", (m, c) =>
            {
                var s = c.Request.Form["book_name"];
                return Text("thanks. i put " + s);
            });

            Delete("book", (m, c) =>
            {
                var s = c.Request.Form["book_name"];
                return Text("thanks. i deleted " + s);
            });

        }
	}
    public class Model
    {
        public string Foo = "hello";
        public DateTime Date = DateTime.Now;
        public List<string> Items = new List<string> {"one", "two", "three"};
        public string Foo1 = "hello";
        public DateTime Date1 = DateTime.Now;
        public List<string> Items1 = new List<string> { "one", "two", "three" };
        public string Foo2 = "hello";
        public DateTime Date2 = DateTime.Now;
        public List<string> Items2 = new List<string> { "one", "two", "three" };
        public string Foo3 = "hello";
        public DateTime Date3 = DateTime.Now;
        public List<string> Items3 = new List<string> { "one", "two", "three" };
        public string Foo4 = "hello";
        public DateTime Date4 = DateTime.Now;
        public List<string> Items4 = new List<string> { "one", "two", "three" };
        public string Foo5 = "hello";
        public DateTime Date5 = DateTime.Now;
        public List<string> Items5 = new List<string> { "one", "two", "three" };
        public string Foo6 = "hello";
        public DateTime Date6 = DateTime.Now;
        public List<string> Items6 = new List<string> { "one", "two", "three" };
        public string Foo7 = "hello";
        public DateTime Date7 = DateTime.Now;
        public List<string> Items7 = new List<string> { "one", "two", "three" };
        public List<string> Items8 = new List<string> { "one", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three", "two", "three" };

    }

    public class Posts : Nina.Application
    {
        public Posts()
        {
            Get("posts", (m, c) =>
            {
                return View("views/posts", "hello posts data");
            });            
        }
    }
}

Nina
====

[Nina][3] is a web microframework for the .Net platform, inspired by Sinatra. It includes several aspects that go futher beyond
Sinatra, such as an abstract, pluggable, and extensible infrastructure.

An important note is that Nina is feature complete. Meaning that unlike open source projects that forge themselves online, it was brewed for a while _before_ opening up to public. Its API stable and well thought out, Nina will not be a moving target for you, and you can count on it.

Nina discussion group:
[http://groups.google.com/group/nina-dev][4]

Features
--------

* A simple to use DSL, a simple programming model.
* Performance as a goal.
* Multiple pluggable view engines.
* Many scenarios covered by Json and XML serialization support.
* Advanced cache-control for a RESTful ecosystem; automatic ETagging and Last-Modified controls.
* Nina is _intentionally_ build against the .Net 2.0 runtime in order to afford a lower barrier of entry!. A port of Nina for the 4.0 runtime will be available shortly.
* _Razor support note_: I chose to use RazorEngine, which is an open-source abstraction of a Razor view engine without using the full MVC stack. In that implementation, view data is exposed as 'Model'. I chose to align other views and now each view has its view data accessible by 'Model' and 'ViewData' members (except Razor).

 
To put some code into words:

    Get("book.xml", (m, c) =>
    {
        Filters.ETag("8");
        return Xml(new Model());
    });

    Get("etagged/crc/book.xml", (m, c) =>
    {
        return Xml(new Model()).ETagged();
    });

    Get("etagged/md5/book.xml", (m, c) =>
    {
        return Xml(new Model()).ETagged(Digest.MD5);
    });
    
    Get("etagged/text", (m, c) =>
    {
        return Text("thanks").ETagged();     
    });
        
    Get("book/{name}", (m, c) =>
    {
        return View("views/foo", new Model()).Created(c.Request.Url.ToString()).ETagged();
    });        
    
    
RESTful 
-------

Nina keeps REST easy, as it was planned to be. Supporting all verbs, through native


    Get("guest/{name}", (m,c) => Text("Hello, " + m["name"]));
    Post("/", (m,c) => View("views/blog", new Blog()));
    Put("/{id}/comments", (m,c) => Json(GetBlogWithComments()));
    Delete("/files/{id}", (m,c) => File(CONFIRMATION));
    

Friendly
--------

Inspired by Sinatra, Nina has a compact and friendly DSL that you'll like from the start.   
    
    
    Post("/", (m,c)=>
    {
      var url = Urls.Save(c.Request.Form["url"]);
      return Text(string.Format(@"<html><body>Your url: 
                <a href='{0}'>{0}</a></body></html>", c.Request.Url +url));
    });

Nina doesn't try to be everything. It does try to give you scenario-based programming
experience with performance to boot: ETagging, cache-control, JSON and XML serialization, growing number of view engines including <a href="http://code.google.com/p/nhaml/">NHaml</a>, <a href="http://ndjango.org/">NDjango</a>, <a href="http://sparkviewengine.com/">Spark</a> and more.

Nina lets you worry about your own code by giving you less decisions to make. In order to be more friendly, sometimes, there is (and should be) only one way to do it.
    

High Performance
----------

Under the hood, Nina is a highly optimized microframework with
comprehensive features and a neat DSL.

To put it into numbers, here is the typical string rendering benchmark.
    
    
    Nina/.NET2.0, IIS 
    Requests per second:    2385.19 [#/sec] (mean)
    
    Sinatra/Ruby1.8.7, Thin 
    Requests per second:    588.33 [#/sec] (mean)
    
Although benchmark comparisons are evil, biased, and so forth, it does give a good feel about running a lightweight framework on windows and IIS.
   
Get Started
-----------

A tutorial is in progress. As well as blog-series explaining Nina's underpinnings (no pun intended).
For now there are several examples for using Nina:

* To quickly get started, you can take TinyUrl demo as a project per-se and start hacking from there.
* To see a full-fledged real world application, check out Panda: a Nina-powered web log, backed with MongoDB with slugs, tagging and comments (disqus).

The TinyUrl Example
-------------------
Here is a self-contained _complete_ Nina application, for a tiny url website. Nice, isn't it?
Check out `Nina.Demo.Tinyurl` to play with it live.
    
    
    //TinyUrl.cs
    public class TinyUrl : Nina.Application
    {
        private static readonly Urls Urls = new Urls();
        public TinyUrl()
        {
            Get("/", (m,c) => Text("<html><body>Tiny url!<form method='post'><input type='text' name='url'/><input type='submit' value='tiny!'/></form></body></html>"));
            
            Post("/", (m,c)=>
            {
                var url = Urls.Save(c.Request.Form["url"]);
                return Text(string.Format("<html><body>Your url: <a href='{0}'>{0}</a></body></html>", c.Request.Url +url));
            });

            Get("/{tinyurl}", (m, c) => Redirect(Urls.Get(m["tinyurl"])) );
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
    




Future plans
------------

Currently Nina is feature complete. It should cover most of the scenarios building RESTful web applications.
However, I do have several more extras on the way which are always

* Initiate a testing roadmap that will not affect Nina's performance.
* Add view engines.
* Add tooling support (NuGet)


Contribute
----------

Nina is an open-source project. Therefore you are free to help improving it.
There are several ways of contributing to Nina's development:

* Build apps using Nina and spread the word.
* Bug and features using the [issue tracker][2].
* Submit patches fixing bugs and implementing new functionality.
* Create a Nina fork on [GitHub][1] and start hacking. Extra points for using GitHubs pull requests and feature branches.

License
-------

This code is free software; you can redistribute it and/or modify it under the
terms of the Apache License. See LICENSE.txt.

Copyright
---------

Copyright (c) 2010, Dotan Nahum <dotan@paracode.com>
    


[1]: http://github.com/jondot/nina
[2]: http://github.com/jondot/nina/issues
[3]: http://jondot.github.com/nina
[4]: http://groups.google.com/group/nina-dev

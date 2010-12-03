Nina
====

[Nina][3] is a *feature complete* web microframework for the .Net platform, inspired by Sinatra. It includes several aspects that go further beyond
Sinatra, such as an abstract, pluggable, and extensible infrastructure.

Features
--------

* A simple to use DSL, a simple programming model.
* Performance as a goal.
* Multiple pluggable view engines.
* Many scenarios covered by Json and XML serialization support.
* Advanced cache-control for a RESTful ecosystem; automatic ETagging and Last-Modified controls.

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
    

Performant
----------

Under the hood, Nina is a highly optimized microframework with
comprehensive features and a neat DSL.

To put it into numbers, here is the typical string rendering benchmark.
    
    
    Requests per second:    2385.19 [#/sec] (mean)<br/>
    Time per request:       0.544 [ms] (mean)
    
   
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

    
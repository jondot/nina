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
using Nina.Panda.Models;
using System.Linq;

namespace Nina.Panda
{
    public class Panda : Nina.Application
    {
        public Panda()
        {
            //https://github.com/rwboyer/scanty/raw/master/public/css/main.css
            Get("/", (m,c) =>
            {
                IEnumerable<Post> findAll = Posts.FindAll(10);
                
                return View("views/index", new PostListViewData(){Blog = new Blog(), Posts = findAll.ToArray(), User=new User()})
                       .ETagged();
            });

            Get("/posts/{year}/{month}/{day}/{slug}", (m, c) =>
            {
                Post post = Posts.Find(m["slug"]);

                return View("views/post", new PostViewData() { Blog = new Blog(), Post = post, User = new User() })
                       .ETagged();
            });

            Get("/posts/{year}/{month}/{day}/{slug}/edit", (m, c) =>
            {
                Post post = Posts.Find(m["slug"]);

                return View("views/edit", new PostViewData() { Blog = new Blog(), Post = post, User = new User(), Url = post.Url });
            });

            Post("/posts/{year}/{month}/{day}/{slug}", (m, c) =>
            {
                Post post = Posts.Find(m["slug"]);
                post.Title = c.Request.Form["title"];
                post.Body = c.Request.Form["body"];
                post.StringifiedTags = c.Request.Form["tags"];
                Posts.Update(post);
                return Redirect(post.Url);
            });

            Get("/posts/new", (m, c) =>
            {
                //todo: auth
                var p = new Post();
                return View("views/edit", new PostViewData() { Post = p, Blog = new Blog(), User = new User(), Url = "/posts" });
            });

            Post("/posts", (m,c)=>
            {
                var slug = Models.Post.Sluggify(c.Request.Form["title"]);
                var post = new Post(){Body = c.Request.Form["body"], Title=c.Request.Form["title"],StringifiedTags= c.Request.Form["tags"], Created=DateTime.Now,Slug=slug };
                Posts.Save(post);
                return Redirect(post.Url);
            });

            Get("/posts/tagged/{tag}", (m,c)=>
            {
                var findAllByTag = Posts.FindAllByTag(m["tag"], 30);
                return View("views/index", new PostListViewData() { Posts = findAllByTag.ToArray(), Blog = new Blog(), User = new User() });
            });
        }
    }

    public class ApplicationViewData
    {
        public Blog Blog { get; set; }
        public User User { get; set; }
    }

    public class PostViewData : ApplicationViewData
    {
        public Post Post { get; set; }
        public string Url { get; set; }
    }
    public class PostListViewData : ApplicationViewData
    {
        public Post [] Posts { get; set; }
    }
  
}
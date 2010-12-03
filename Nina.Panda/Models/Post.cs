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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using MarkdownSharp;
using MongoDB.Bson;
using MongoDB.Bson.DefaultSerializer;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Nina.Panda.Models
{
    internal class Posts
    {
        private static MongoDatabase _mongoDatabase;
        private static MongoCollection<Post> _mongoCollection;

        static Posts()
        {
            MongoServer server = MongoServer.Create();
            _mongoDatabase = server.GetDatabase("panda");
            _mongoCollection = _mongoDatabase.GetCollection<Post>("posts");
        }

        public static IEnumerable<Post> FindAll()
        {
            return _mongoCollection.FindAll().SetSortOrder(SortBy.Descending("Created"));
        }
        public static IEnumerable<Post> FindAll(int top)
        {
            return FindAll(0, top);
        }
        public static IEnumerable<Post> FindAll(int skip, int take)
        {
            return _mongoCollection.FindAll().SetSortOrder(SortBy.Descending("Created")).SetSkip(skip).SetLimit(take);
        }
        public static IEnumerable<Post> FindAllByTag(string tag, int top)
        {
            return _mongoCollection.Find(Query.EQ("Tags",tag)).SetLimit(top);
        }
        public static Post Find(string slug)
        {
            return _mongoCollection.FindOne(Query.EQ("Slug", slug));
        }
        public static Post Save(Post p)
        {
            p.BodyHtml = GetBodyHtml(p);
            p.SummaryHtml = GetSummaryHtml(p);
            _mongoCollection.Insert(p);
            return p;
        }

        public static void Update(Post p)
        {
            p.BodyHtml = GetBodyHtml(p);
            p.SummaryHtml = GetSummaryHtml(p);
            _mongoCollection.Update(Query.EQ("_id", p.Id), p);
        }

        private static string GetSummaryHtml(Post p)
        {
            if (string.IsNullOrEmpty(p.Body))
                return string.Empty;

            var match = Regex.Match(p.Body, "(.{200}.*?\n)");
            return new Markdown().Transform(match.Success ? match.Captures[0].Value : p.Body);
        }
        private static string GetBodyHtml(Post p)
        {
            if(string.IsNullOrEmpty(p.Body))
                    return string.Empty;
                return new Markdown().Transform(p.Body); 
        }

    }

    public class Post
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string [] Tags { get; set; }
        public DateTime Created { get; set; }
        public string SummaryHtml { get; set; }
        public string BodyHtml { get; set; }

        [BsonIgnore]
        public string Url { get { return string.Format("/posts/{0}/{1}/{2}/{3}", Created.Year, Created.Month, Created.Day, Slug); } }
        [BsonIgnore]
        public bool IsMore { get { return Body.Length > 200; } }
        [BsonIgnore]
        public string LinkedTags { get
        {
            if (Tags == null)
                return string.Empty;
            var sb =new StringBuilder();
            foreach (var tag in Tags)
            {
                sb.Append("<a href='/posts/tagged/").Append(tag).Append("'>").Append(tag).Append("</a> ");
            }
            return sb.ToString();
        }}
        [BsonIgnore]
        public string StringifiedTags
        {
            get
            {
                return Tags == null || Tags.Length == 0 ? string.Empty : string.Join(",", Tags);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                var strings = value.Split(',');
                Tags = strings.Select(x => x.Trim()).ToArray();
            }
        }

        public static string Sluggify(string title)

        {
            Random random = new Random(DateTime.Now.Second);
            var replace = random.Next(999)+"-"+title.Trim().ToLower().Replace(' ', '-');
            return Regex.Replace(replace, "[^a-z0-9_\\-]","");
        }
    }
}
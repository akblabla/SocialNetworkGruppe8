using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkGruppe8.Models;
using MongoDB.Driver;

namespace ConsoleApp2.Services
{
    class FeedService
    {
        private readonly IMongoCollection<Feed> _feed;

        public FeedService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("FeedDb");
            _feed = database.GetCollection<Feed>("Feed");
        }

        public List<Feed> Get()
        {
            return _feed.Find(book => true).ToList();
        }

        public Feed Get(string id)
        {
            return _feed.Find<Feed>(feed=> feed.Id == id).FirstOrDefault();
        }

        public Feed Create(Feed feed)
        {
            _feed.InsertOne(feed);
            return feed;
        }

        public void Update(string id, Feed feedIn)
        {
            _feed.ReplaceOne(feed => feed.Id == id, feedIn);
        }

        public void Remove(Feed feedIn)
        {
            _feed.DeleteOne(feed => feed.Id == feedIn.Id);
        }

        public void Remove(string id)
        {
            _feed.DeleteOne(feed => feed.Id == id);
        }
    }
}

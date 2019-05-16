using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkGruppe8.Models;
using MongoDB.Driver;

namespace SocialNetworkGruppe8.Services
{
    public class SvendService
    {
        private IMongoCollection<User> _users;
        private IMongoCollection<Comment> _comments;
        private IMongoCollection<Post> _posts;
        private IMongoDatabase _database;

        public SvendService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("SvendDb");
        }

        // view comments for post

        public List<User> Get() // this is just made as a test. we should have this
        {
            return _users.Find(book => true).ToList();
        }

        //public Feed Get(string id)
        //{
        //    return _feed.Find<Feed>(feed=> feed.Id == id).FirstOrDefault();
        //}

        public User Create(User user)
        {
            if (_users == null)
            {
                _users = _database.GetCollection<User>("Users");
            }
            _users.InsertOne(user);
            return user;
        }

        //public void Update(string id, Feed feedIn)
        //{
        //    _feed.ReplaceOne(feed => feed.Id == id, feedIn);
        //}

        //public void Remove(Feed feedIn)
        //{
        //    _feed.DeleteOne(feed => feed.Id == feedIn.Id);
        //}

        //public void Remove(string id)
        //{
        //    _feed.DeleteOne(feed => feed.Id == id);
        //}
    }
}

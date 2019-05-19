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

            _users = _database.GetCollection<User>("Users");
            _comments = _database.GetCollection<Comment>("Comments");
            _posts = _database.GetCollection<Post>("Posts");
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        //public User Update(string userId, User user)
        //{
        //    _users.UpdateOne(u => userId == u.Id, new ObjectUpdateDefinition<User>(user));
        //    return user;
        //}

        public Comment Create(string postId, Comment comment)
        {
            _comments.InsertOne(comment);
            return comment;
        }

        public Post Create(string ownerId, Post post)
        {
            _posts.InsertOne(post);


            var user = _users.Find<User>(u => u.Id == ownerId).FirstOrDefault();
            foreach (var followerId in user.Followers)
            {
                var follower = _users.Find<User>(u => u.Id == followerId).FirstOrDefault();
                if (follower != null)
                {

                    if (post.IsPublic)
                    {
                        follower.Feed.Add(post);
                        if (follower.Feed.Count > 2)
                        {
                            _users.UpdateOne(u => follower.Id == u.Id, new ObjectUpdateDefinition<User>(follower));//nope? should be post
                        }
                    }
                    else if (user.UserCircle.Any(id => id == follower.Id))
                    {
                        follower.Feed.Add(post);
                        if (follower.Feed.Count > 2)
                        {
                            _users.UpdateOne(u => follower.Id == u.Id, new ObjectUpdateDefinition<User>(follower));//nope? should be post
                        }
                    }
                }

            }
            return post;
        }

        public IEnumerable<Tuple<Post, IEnumerable<Comment>>> GetWall(string userId, string visitorId)
        {
            var user = _users.Find<User>(u => u.Id == userId).FirstOrDefault();
            if (user != null && user.UserCircle != null)
            {

                bool isVisitorInCircle = (bool)user?.UserCircle?.Any(v => v == visitorId);

                if (isVisitorInCircle)
                {
                    var posts = _posts.Find<Post>(p => p.UserId == userId).ToList();
                    var result = new List<Tuple<Post, IEnumerable<Comment>>>();
                    foreach (var post in posts)
                    {
                        var comments = _comments.Find<Comment>(c => c.PostId == post.Id).ToList();
                        result.Add(new Tuple<Post, IEnumerable<Comment>>(post, comments));
                    }
                    return result;
                }
                else
                {
                    var posts = _posts.Find<Post>(p => p.UserId == userId && p.IsPublic).ToList();
                    var result = new List<Tuple<Post, IEnumerable<Comment>>>();
                    foreach (var post in posts)
                    {
                        var comments = _comments.Find<Comment>(c => c.PostId == post.Id).ToList();
                        result.Add(new Tuple<Post, IEnumerable<Comment>>(post, comments));
                    }
                    return result;
                }
            }
            return new List<Tuple<Post, IEnumerable<Comment>>>();
        }

        public List<User> Get() // this is just made as a test. we should have this
        {
            return _users.Find(u => true).ToList();
        }
    }
}

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
            _comments = _database.GetCollection<Comment>("Users");
            _posts = _database.GetCollection<Post>("Posts");
        }

        // view comments for post

        //public Feed Get(string id)
        //{
        //    return _feed.Find<Feed>(feed=> feed.Id == id).FirstOrDefault();
        //}

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        //public Post Create(Post post)
        //{
        //    _posts.InsertOne(post);
        //    // add to feed
        //    return post;
        //}

        public Comment Create(string postId, string commentText)
        {
            Comment comment = new Comment()
            {
                Text = commentText,
                PostId = postId
            };

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
                            _users.UpdateOne(u => follower.Id == u.Id, new ObjectUpdateDefinition<User>(follower));
                        }
                    }
                    else if (user.UserCircle.Any(id => id == follower.Id))
                    {
                        follower.Feed.Add(post);
                        if (follower.Feed.Count > 2)
                        {
                            _users.UpdateOne(u => follower.Id == u.Id, new ObjectUpdateDefinition<User>(follower));
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

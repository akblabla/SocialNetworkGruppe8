using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetworkGruppe8.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        // other user info
        public ICollection<string> UserSubscribers { get; set; } // string id or user
        public ICollection<string> UserCircle { get; set; } // string id or user
        public ICollection<Post> Feed { get; set; } // With max amount constraint!

    }
}

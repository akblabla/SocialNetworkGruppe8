using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections;
using SocialNetworkGruppe8.Models;

namespace SocialNetworkGruppe8.Models
{
    class Feed
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Post")]
        public Post Posts { get; set; }
    }
}
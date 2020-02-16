using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acclaim.Api.Domain.MongoDomains
{
    public class MongoAcclaim
    {
        public MongoAcclaim()
        {
            RelatedAcclaims = new List<RelatedAcclaims>();
            Users = new List<User>();
            AcclaimRules = new List<AcclaimRule>();
        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AcclaimLogo { get; set; }
        public AcclaimType AcclaimType { get; set; }
        public List<AcclaimRule> AcclaimRules { get; set; }
        public List<RelatedAcclaims> RelatedAcclaims { get; set; }
        public List<User> Users { get; set; }
        public AcclaimProvider AcclaimProvider { get; set; }
        public AProvider AProvider { get; set; }
        public Issuer Issuer { get; set; }

    }
}

using System;

namespace Acclaim.Api.Domain.MongoDomains
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfAcclaim { get; set; }
    }
}
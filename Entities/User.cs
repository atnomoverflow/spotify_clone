using System;

namespace spotify_clone.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
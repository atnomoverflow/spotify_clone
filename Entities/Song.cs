using System;
namespace spotify_clone.Entities
{
    public record Song
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public float Duration { get; set; }

    }
} 
using System.Diagnostics.CodeAnalysis;

namespace BookMyShow.Models
{
    [ExcludeFromCodeCoverage]
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timing { get; set; }
        public bool IsBooked { get; set; }
    }
}

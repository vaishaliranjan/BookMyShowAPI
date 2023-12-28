using System.Diagnostics.CodeAnalysis;

namespace BookMyShow.Models
{
    [ExcludeFromCodeCoverage]
    public class Venue
    {
        public int VenueId { get; set; }
        public string Place { get; set; }
        public bool IsBooked { get; set; }
    }
}

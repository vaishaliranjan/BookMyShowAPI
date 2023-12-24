using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMyShow.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        [JsonIgnore]
        public Artist Artist { get; set; }
        [ForeignKey("Venue")]
        public int VenueId { get; set; }
        [JsonIgnore]
        public Venue Venue { get; set; }
        public int InitialTickets { get; set; }
        public int NumberOfTickets { get; set; }
        public double Price { get; set; }
        [ForeignKey("User")]
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}

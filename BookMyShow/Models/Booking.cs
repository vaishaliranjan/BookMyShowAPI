
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMyShow.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int NumberOfTickets { get; set; }
        [JsonIgnore]
        public double TotalPrice { get; set; }
    }
}

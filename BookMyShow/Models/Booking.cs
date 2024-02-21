using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BookMyShow.Models
{
    [ExcludeFromCodeCoverage]
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
        [ForeignKey("User")]
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int NumberOfTickets { get; set; }
        [JsonIgnore]
        public double TotalPrice { get; set; }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace BookMyShow.Models
{
    public class BookingWithEvent
    {
        public Booking Booking { get; set; }
        public Event Event { get; set; }

        public string Username { get; set; }
        public string UserId { get; set; }
    }

}

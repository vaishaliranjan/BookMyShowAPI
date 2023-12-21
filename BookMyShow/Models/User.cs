using BookMyShow.Models.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookMyShow.Models
{
    public class User
    {
        [Key, ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public string Username { get; set; }
        [JsonIgnore]
        public IdentityUser IdentityUser { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Event> Events { get; set; }
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }


    }
}

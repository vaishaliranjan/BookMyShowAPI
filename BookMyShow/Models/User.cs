using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyShow.Models
{
    public class User
    {
        [Key, ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public string Username { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Booking> Bookings { get; set; }


    }
}

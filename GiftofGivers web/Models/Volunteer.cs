using System;
using System.ComponentModel.DataAnnotations;

namespace GiftofGivers_web.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string TaskAssigned { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}

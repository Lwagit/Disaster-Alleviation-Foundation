using System;
using System.ComponentModel.DataAnnotations;

namespace GiftofGivers_web.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required]
        public string DisasterType { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime ReportedAt { get; set; } = DateTime.Now;
    }
}

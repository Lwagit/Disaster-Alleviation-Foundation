using System;
using System.ComponentModel.DataAnnotations;

namespace GiftofGivers_web.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public string ResourceType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string DonorName { get; set; }
        public string DonorContact { get; set; }

        public DateTime DonatedAt { get; set; } = DateTime.Now;
    }
}

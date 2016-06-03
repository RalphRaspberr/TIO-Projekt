using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Manager.Models
{
    public class Statistics
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaticticsId { get; set; }

        [Required]
        public DateTime DateOfView { get; set; }

        [Required]
        public IPAddress ViewerIpAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserService.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        public string Nick { get; set; }

        [DataMember]
        [Required]
        public string PasswordSHA1 { get; set; }
    }
}
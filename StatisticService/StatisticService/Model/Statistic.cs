using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Runtime.Serialization;

namespace StatisticService.Model
{
    [DataContract]
    public class Statistic
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String ImageId { get; set; }

        [DataMember]
        public DateTime ViewDate { get; set; }

        [DataMember]
        public string Author { get; set; }
    }   
}
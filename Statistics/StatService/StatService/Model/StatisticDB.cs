using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace StatService.Model
{
    public class StatisticDB
    {
        public int Id { get; set; }
        public String ImageId { get; set; }
        public DateTime ViewDate { get; set; }
        public IPAddress UserIp { get; set; }
        public int UserId { get; set; }
    }
}
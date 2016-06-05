using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace StatisticService.Model
{
    public class Statistic
    {
        public int Id { get; }
        public String ImageId { get; set; }
        public DateTime ViewDate { get; set; }
        public IPAddress UserIp { get; set; }
        public int UserId { get; set; }

        public Statistic(String imageId, DateTime viewDate, IPAddress userIp, int userId)
        {
            this.ImageId = imageId;
            this.ViewDate = viewDate;
            this.UserIp = userIp;
            this.UserId = UserId;
        }
    }   
}
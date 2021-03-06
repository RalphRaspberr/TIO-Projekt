﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Manager.StaticticsRepository;

namespace Manager.Controllers
{
    [RoutePrefix("api/Statistics")]
    public class StatisticsController : ApiController
    {
        private StatServiceClient _stats = new StatServiceClient();

        // GET: api/Ada/imageAdy
        [HttpGet]
        [Route("{authorName}/{imageId}")]
        public int Get(string authorName, string imageId)
        {
            return _stats.FindImageStats(imageId, authorName).Length;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StatisticService.Repositories;
using StatisticService.Model;

namespace StatisticService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IStatService
    {
        private readonly IStatisticRepository statisticsRepository;

        public Service1()
        {
            this.statisticsRepository = new StatisticRepository();
        }



        public int AddStatitics(Statistic statistic)
        {
            return this.statisticsRepository.Add(statistic);
        }

        public IEnumerable<Statistic> FindImageStats(string imageId, string authorName)
        {
            return this.statisticsRepository.FindImageStats(imageId, authorName);
        }

        public List<Statistic> FindAll()
        {
            return this.statisticsRepository.FindAll();
        }

    }
        
}

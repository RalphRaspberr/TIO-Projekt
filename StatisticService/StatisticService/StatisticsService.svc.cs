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
        private readonly StatisticRepository statisticsRepository;

        public Service1()
        {
            this.statisticsRepository = new StatisticRepositoryImpl();
        }

        public int AddStatitics(Statistic statistic)
        {
            return this.statisticsRepository.Add(statistic);
        }

        public List<Statistic> GetAllImageViewStatistics(String id)
        {
            return this.statisticsRepository.findAllByImageId(id);
        }

        public List<Statistic> GetAllViewStatistics()
        {
            return this.statisticsRepository.findAll();
        }

    }
        
}

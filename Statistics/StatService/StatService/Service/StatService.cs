using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatService.Model;
using System.ServiceModel;
using StatService.Repositories;

namespace StatService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class StatService : IStatService
    {
        private readonly IStatisticsRepository statisticsRepository;

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

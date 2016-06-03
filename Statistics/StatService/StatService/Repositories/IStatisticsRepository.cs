using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatService.Model;

namespace StatService.Repositories
{
    public interface IStatisticsRepository
    {
        List<Statistic> findAll();
        List<Statistic> findAllByImageId(String id);
        int Add(Statistic statistic);
    }
}

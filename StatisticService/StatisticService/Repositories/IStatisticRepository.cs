using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticService.Model;

namespace StatisticService.Repositories
{
    public interface IStatisticRepository
    {
        List<Statistic> findAll();
        List<Statistic> findAllByImageId(String id);
        int Add(Statistic statistic);
    }
}

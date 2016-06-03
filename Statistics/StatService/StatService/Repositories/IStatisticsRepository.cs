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
        List<Statistic> fingAll();
        int Add(Statistic statistic);
        Statistic findOne(int id);
        Statistic Update(Statistic statistic);
        bool Delete(int id);
    }
}

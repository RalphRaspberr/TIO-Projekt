using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using StatisticService.Model;

namespace StatisticService.Repositories
{
    public interface IStatisticRepository
    {
        int Add(Statistic statistic);

        List<Statistic> FindAll();

        IEnumerable<Statistic> FindImageStats(String imageId, String authorName);

    }
}

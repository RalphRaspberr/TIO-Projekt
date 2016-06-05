using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using StatisticService.Model;

namespace StatisticService.Repositories
{
    [ServiceContract]
    public interface IStatisticRepository
    {
        [OperationContract]
        List<Statistic> findAll();

        [OperationContract]
        List<Statistic> findAllByImageId(String id);

        [OperationContract]
        int Add(Statistic statistic);
    }
}

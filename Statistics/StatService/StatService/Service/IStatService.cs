using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatService.Model;
using System.ServiceModel;

namespace StatService.Service
{
    [ServiceContract]
    public interface IStatService
    {
        [OperationContract]
        int AddStatitics(Statistic statistic);

        [OperationContract]
        List<Statistic> GetAllImageViewStatistics(String id);

        [OperationContract]
        List<Statistic> GetAllViewStatistics();
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StatisticService.Model;

namespace StatisticService
{
    [ServiceContract]
    public interface IStatService
    {
        [OperationContract]
        int AddStatitics(Statistic statistic);

        [OperationContract]
        IEnumerable<Statistic> FindImageStats(string imageId, string authorName );

        [OperationContract]
        List<Statistic> FindAll();
    }
}


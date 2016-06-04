using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LogService
{
    [ServiceContract]
    public interface ILoggerService
    {

        [OperationContract]
        void addLog(string message, LogLevel logLevel);

    }
}

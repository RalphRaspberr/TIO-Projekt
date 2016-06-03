using ImageService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    [ServiceContract]
    public interface IGraphicRepository
    {
        [OperationContract]
        string AddImage(Graphic graphic);

        [OperationContract]
        Graphic GetImage(int user, string id);

        [OperationContract]
        IEnumerable<Graphic> GetNewestImages(int limit);

        [OperationContract]
        IEnumerable<Graphic> GetUserImages(int user);
    }
}

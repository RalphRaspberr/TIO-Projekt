using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using ImageService.Models;

namespace ImageService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GraphicRepository : IGraphicRepository
    {
        public string AddImage(Graphic graphic)
        {
            return "";
        }

        public Graphic GetImage(int user, string id)
        {
            throw new NotImplementedException();
        }

        public Graphic GetNewestImages(int limit)
        {
            throw new NotImplementedException();
        }

        public Graphic GetUserImages(int user)
        {
            throw new NotImplementedException();
        }
    }
}
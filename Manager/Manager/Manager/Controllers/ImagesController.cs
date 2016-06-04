using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Manager.GraphicRepository;

namespace Manager.Controllers
{
    public class ImagesController : ApiController
    {
        private GraphicRepositoryClient _repo = new GraphicRepositoryClient();

        // GET: api/Images/?limit=10
        public IEnumerable<Graphic> GetNewestImages([FromUri] int limit)
        {
            return _repo.GetNewestImages(limit);
        }

        // GET: api/Images/?userId=10&imageId=abc
        public Graphic GetAuthorsImage([FromUri] ImageAndItsAuthor img)
        {
            return _repo.GetUserImages(img.userId).First(i => i.Id == img.imageId);          
        }

        // GET: api/Images/?userId=10
        public IEnumerable<Graphic> GetAuthorImages([FromUri] int userId)
        {
            return _repo.GetUserImages(userId);
        }

        // POST: api/Images
        public void PostImage([FromBody]Graphic graphic)
        {
            _repo.AddImage(graphic);
        }

        public class ImageAndItsAuthor
        {
            public int userId;
            public string imageId;
        }
    }
}

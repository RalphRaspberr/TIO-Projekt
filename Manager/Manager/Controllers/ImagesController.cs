using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        [Authorize]
        public HttpResponseMessage PostFormData()
        {         
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    Graphic imgToAdd = new Graphic()
                    {
                        Title = httpRequest.Form["Title"],
                        Author = Int32.Parse(httpRequest.Form["AuthorId"]),
                        ImageStream = httpRequest.Files[0].InputStream
                    };
                    _repo.AddImage(imgToAdd);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public class ImageAndItsAuthor
        {
            public int userId;
            public string imageId;
        }
    }
}

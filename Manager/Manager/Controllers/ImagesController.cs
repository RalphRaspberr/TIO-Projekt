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
using Manager.LoggingService;

namespace Manager.Controllers
{
    public class ImagesController : ApiController
    {
        private GraphicRepositoryClient _repo = new GraphicRepositoryClient();
        private LoggerServiceClient log = new LoggerServiceClient();

        // GET: api/Images/?limit=10
        public IEnumerable<Graphic> GetNewestImages([FromUri] int limit)
        {
            log.addLog($"ImagesController: GET was called - GET: api/Images/?limit={limit}", LogLevel.INFO);
            return _repo.GetNewestImages(limit);
        }

        // GET: api/Images/?userId=10&imageId=abc
        public Graphic GetAuthorsImage([FromUri] ImageAndItsAuthor img)
        {
            log.addLog($"ImagesController: GET was called - GET: api/Images/?userId={img.userId}&imageId={img.imageId}", LogLevel.INFO);
            return _repo.GetUserImages(img.userId).First(i => i.Id == img.imageId);          
        }

        // GET: api/Images/?userId=10
        public IEnumerable<Graphic> GetAuthorImages([FromUri] int userId)
        {
            log.addLog($"ImagesController: GET was called - GET: api/Images/?userId={userId}", LogLevel.INFO);
            return _repo.GetUserImages(userId);
        }


        // POST: api/Images
        [Authorize]
        public HttpResponseMessage PostFormData()
        {
            log.addLog($"ImagesController: GET was called - GET: api/Images", LogLevel.INFO);
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
                    log.addLog("ImagesController: new image added to the repository: " +
                               $"Id={imgToAdd.Id} , Title={imgToAdd.Title}, Author={imgToAdd.Author}", LogLevel.INFO);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            log.addLog("ImagesController: POST was called - GET: api/Images , but there was no files in HTTP request.", LogLevel.ERROR);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public class ImageAndItsAuthor
        {
            public int userId;
            public string imageId;
        }
    }
}

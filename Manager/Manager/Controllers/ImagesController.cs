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
using Manager.StaticticsRepository;

namespace Manager.Controllers
{
    public class ImagesController : ApiController
    {
        private GraphicRepositoryClient _repo = new GraphicRepositoryClient();
        private LoggerServiceClient _log = new LoggerServiceClient();
        private StatServiceClient stats = new StatServiceClient();

        // GET: api/Images/?limit=10
        public IEnumerable<Graphic> GetNewestImages([FromUri] int limit)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?limit={limit}", LogLevel.INFO);
            var newestImages = _repo.GetNewestImages(limit);
            foreach (var img in newestImages)
            {
                stats.AddStatitics(new Statistic()
                {
                    ImageId = img.Id,
                    UserId = img.Author,
                    //UserIp = Request.Headers.Host
                });
            }

            return newestImages;
        }

        // GET: api/Images/?userId=10&imageId=abc
        public Graphic GetAuthorsImage([FromUri] ImageAndItsAuthor img)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?userId={img.userId}&imageId={img.imageId}", LogLevel.INFO);
            return _repo.GetUserImages(img.userId).First(i => i.Id == img.imageId);          
        }

        // GET: api/Images/?userId=10
        public IEnumerable<Graphic> GetAuthorImages([FromUri] int userId)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?userId={userId}", LogLevel.INFO);
            return _repo.GetUserImages(userId);
        }


        // POST: api/Images
        [Authorize]
        public HttpResponseMessage PostFormData()
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images", LogLevel.INFO);
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
                    _log.addLog("ImagesController: new image added to the repository: " +
                               $"Id={imgToAdd.Id} , Title={imgToAdd.Title}, Author={imgToAdd.Author}", LogLevel.INFO);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            _log.addLog("ImagesController: POST was called - GET: api/Images , but there was no files in HTTP request.", LogLevel.ERROR);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public class ImageAndItsAuthor
        {
            public int userId;
            public string imageId;
        }
    }
}

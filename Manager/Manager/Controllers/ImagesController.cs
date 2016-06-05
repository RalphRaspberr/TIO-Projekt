using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Manager.GraphicRepository;
using Manager.LoggingService;
using Manager.StaticticsRepository;
using WebGrease.Css.Extensions;

namespace Manager.Controllers
{
    [RoutePrefix("api/Images")]
    public class ImagesController : ApiController
    {
        private GraphicRepositoryClient _repo = new GraphicRepositoryClient();
        private LoggerServiceClient _log = new LoggerServiceClient();
        private StatServiceClient stats = new StatServiceClient();

        // GET: api/Images
        [HttpGet]
        [Route("")]
        public IEnumerable<Graphic> GetImages()
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images", LogLevel.INFO);

            //Default 10 images
            var newestTenImages = _repo.GetNewestImages(10);
            //Add statistics
            newestTenImages?.ForEach(i => AddStats(i.Id, i.Author));

            return newestTenImages;
        }

        // GET: api/Images/?limit=10
        [HttpGet]
        [Route("{limit}")]
        public IEnumerable<Graphic> GetNewestImages(int limit)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?limit={limit}", LogLevel.INFO);

            var newestImages = _repo.GetNewestImages(limit);
            //Add statistics
            newestImages?.ForEach(i => AddStats(i.Id, i.Author));

            return newestImages;
        }

        // GET: api/Images/?authorName=10&imageId=abc
        [HttpGet]
        [Route("{authorName}/{imageId}")]
        public Graphic GetAuthorsImage(string authorName, string imageId)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?authorName={authorName}&imageId={imageId}", LogLevel.INFO);

            var authorsImage = _repo.GetUserImages(authorName).First(i => i.Id == imageId);

            // Add statistics   
            if (authorsImage != null)
            {                
                AddStats(authorsImage.Id, authorsImage.Author);
            }
            return authorsImage;       
        }

        // GET: api/Images/?authorName=Ada
        [HttpGet]
        [Route("{authorName}")]
        public IEnumerable<Graphic> GetAuthorImages( string authorName)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?authorName={authorName}", LogLevel.INFO);

            var authorsImages = _repo.GetUserImages(authorName);
            //Add statistics
            authorsImages?.ForEach(i => AddStats(i.Id, i.Author));
            
            return authorsImages;
        }

        // POST: api/Images
        [HttpPost]
        [Route("")]
        [Authorize]
        public HttpResponseMessage PostFormData()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[0];
                if (postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };

                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    string path = "";

                    if (!AllowedFileExtensions.Contains(extension))
                    {                           
                        _log.addLog("ImagesController: attempt to add file that is not an image of extension .jpg, .gif, or .png", LogLevel.ERROR);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please Upload image of type .jpg,.gif,.png.");
                    }
                    else if (postedFile.ContentLength > MaxContentLength)
                    {
                        _log.addLog("ImagesController: attempt to add file that is greater than 10MB", LogLevel.ERROR);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please Upload a file upto 10 MB.");
                    }
                    else
                    {
                        var imgToAdd = new Graphic()
                        {
                            Title = httpRequest.Form["Title"],
                            Author = httpRequest.Form["Author"],
                            Bytes = postedFile.InputStream.ReadFully()
                        };
                        _log.addLog($"ImagesController: added image: Title = {imgToAdd.Title}, Author = {imgToAdd.Author} ",LogLevel.INFO);
                        path = _repo.AddImage(imgToAdd);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.Created, path);
                }
                var res = "Please Upload a image.";
                return Request.CreateResponse(HttpStatusCode.NotFound, res);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public class ImageAndItsAuthor
        {
            public string AuthorName;
            public string ImageId;
        }

        private void AddStats(string imageId, string author)
        {
            stats.AddStatitics(new Statistic()
            {
                ImageId = imageId,
                Author = author,
                ViewDate = DateTime.Now
            });
        }     
    }

    public static class Extensions
    {
        public static byte[] ReadFully(this Stream input)
        {
            byte[] buffer = new byte[10 * 1024 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }


}

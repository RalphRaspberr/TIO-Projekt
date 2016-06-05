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
            //foreach (var img in newestImages)
            //{
            //    stats.AddStatitics(new Statistic()
            //    {
            //        ImageId = img.Id,
            //        UserId = img.Author,
                    
            //        //UserIp = Request.Headers.Host
            //    });
            //}

            return newestImages;
        }

        // GET: api/Images/?authorName=10&imageId=abc
        public Graphic GetAuthorsImage([FromUri] ImageAndItsAuthor img)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?authorName={img.authorName}&imageId={img.imageId}", LogLevel.INFO);
            return _repo.GetUserImages(img.authorName).First(i => i.Id == img.imageId);          
        }

        // GET: api/Images/?authorName=10
        public IEnumerable<Graphic> GetAuthorImages([FromUri] string authorName)
        {
            _log.addLog($"ImagesController: GET was called - GET: api/Images/?authorName={authorName}", LogLevel.INFO);
            return _repo.GetUserImages(authorName);
        }

        // POST: api/Images
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
                    int MaxContentLength = 1024 * 1024 * 1; //Size = 10 MB

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };

                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

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
                            ImageStream = postedFile.InputStream
                        };
                        _log.addLog($"ImagesController: added image: Title = {imgToAdd.Title}, Author = {imgToAdd.Author} ",LogLevel.INFO);
                        _repo.AddImage(imgToAdd);
                    }
                    var message1 = "Image Updated Successfully.";
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
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
            public string authorName;
            public string imageId;
        }
    }
}

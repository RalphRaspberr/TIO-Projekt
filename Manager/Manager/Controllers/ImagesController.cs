using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            //foreach (var img in newestImages)
            //{
            //    stats.AddStatitics(new Statistic()
            //    {
            //        ImageId = img.Id,
            //        UserId = img.Author,
            //        //UserIp = Request.Headers.Host
            //   });
            //}

            return newestImages;
        }

        // GET: api/Images/?userId=10&imageId=abc
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

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 10 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {                           
                            _log.addLog("ImagesController: attempt to add file that is not an image of type .jpg, .gif, or .png", LogLevel.ERROR);
                            var message = "Please Upload image of type .jpg,.gif,.png.";
                            return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = "Please Upload a file upto 10 MB.";
                            return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                        }
                        else
                        {
                            var imgToAdd = new Graphic()
                            {
                                Title = httpRequest.Form["Title"],
                                Author = httpRequest.Form["Author"],
                                ImageStream = httpRequest.Form["Image"].ToStream()
                            };
                            _repo.AddImage(imgToAdd);
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
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

    public static class Extensions
    {
        public static Stream ToStream(this object str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}

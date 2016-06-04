using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using ImageService.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace ImageService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GraphicRepository : IGraphicRepository
    {
        public string AddImage(Graphic graphic)
        {
            string path = $"storage/{graphic.Author}/{graphic.Id}.jpg";

            Image img = Image.FromStream(graphic.ImageStream);
            img.Save(path, ImageFormat.Jpeg);

            return path;
        }

        public Graphic GetImage(int author, string id)
        {
            string path = $"storage/{author}/{id}.jpg";
            byte[] titleBytes = System.Convert.FromBase64String(id);
            string title = System.Text.Encoding.UTF8.GetString(titleBytes);

            return new Graphic(author, title, id, path);
        }

        public IEnumerable<Graphic> GetNewestImages(int limit)
        {
            List<Graphic> graphics = new List<Graphic>();
            string[] files = Directory.GetFiles(@"storage/", "*.*", SearchOption.AllDirectories);

            int author;
            string path;
            string id;
            byte[] titleBytes;
            string title;
            foreach (string file in files)
            {
                Match match = Regex.Match(file, @"/storage/([^/] +)/([^\.] +)$", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    author = int.Parse(match.Groups[1].Value);
                    id = match.Groups[2].Value;
                    titleBytes = System.Convert.FromBase64String(id);
                    title = System.Text.Encoding.UTF8.GetString(titleBytes);
                    path = $"storage/{author}/{id}.jpg";
                    graphics.Add(new Graphic(author, title, id, path));
                }

                if (graphics.Count >= limit)
                {
                    break;
                }
            }

            return graphics;
        }

        public IEnumerable<Graphic> GetUserImages(int author)
        {
            List<Graphic> graphics = new List<Graphic>();
            string[] files = Directory.GetFiles($"storage/{author}/", "*.*", SearchOption.AllDirectories);

            string path;
            string id;
            byte[] titleBytes;
            string title;
            foreach (string file in files)
            {
                Match match = Regex.Match(file, @"/storage/([^/] +)/([^\.] +)$", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    id = match.Groups[2].Value;
                    titleBytes = System.Convert.FromBase64String(id);
                    title = System.Text.Encoding.UTF8.GetString(titleBytes);
                    path = $"storage/{author}/{id}.jpg";
                    graphics.Add(new Graphic(author, title, id, path));
                }
            }

            return graphics;
        }
    }
}
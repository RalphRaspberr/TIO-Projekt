using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using ImageService.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;

namespace ImageService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class GraphicRepository : IGraphicRepository
    {
        public string AddImage(Graphic graphic)
        {
            byte[] authorBytes = System.Text.Encoding.UTF8.GetBytes(graphic.Author);
            string authorBase64 = System.Convert.ToBase64String(authorBytes);
            string path = $"storage\\{authorBase64}\\{graphic.Id}.jpg";
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Directory.CreateDirectory(currentDirectory + $"\\storage");
            Directory.CreateDirectory(currentDirectory + $"\\storage\\{authorBase64}");

            Image img = Image.FromStream(new MemoryStream(graphic.Bytes));
            img.Save(currentDirectory + "\\" + path, ImageFormat.Jpeg);

            return $"storage/{authorBase64}/{graphic.Id}.jpg";
        }

        /// <summary>
        /// Returns Graphic model based on author name and image ID.
        /// </summary>
        /// <param name="author">Author name.</param>
        /// <param name="id">Image ID.</param>
        /// <returns>Graphic model.</returns>
        public Graphic GetImage(string author, string id)
        {
            byte[] titleBytes = System.Convert.FromBase64String(id);
            string title = System.Text.Encoding.UTF8.GetString(titleBytes);

            byte[] authorBytes = System.Text.Encoding.UTF8.GetBytes(author);
            string authorBase64 = System.Convert.ToBase64String(authorBytes);

            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            string path = $"storage/{authorBase64}/{id}.jpg";

            if(File.Exists(currentDirectory + $"\\storage\\{authorBase64}\\{id}.jpg"))
            {
                return new Graphic(author, title, id, path);
            }

            return null;
        }

        public IEnumerable<Graphic> GetNewestImages(int limit)
        {
            List<Graphic> graphics = new List<Graphic>();
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            FileInfo[] files = Directory.GetFiles(currentDirectory + @"storage\", "*.*", SearchOption.AllDirectories)
                .Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).ToArray();
            string authorBase64;
            byte[] authorBytes;
            string author;
            string path;
            string titleBase64;
            byte[] titleBytes;
            string title;
            foreach (FileInfo file in files)
            {
                authorBase64 = Path.GetFileName(file.DirectoryName);
                titleBase64 = Path.GetFileNameWithoutExtension(file.Name);

                authorBytes = System.Convert.FromBase64String(authorBase64);
                author = System.Text.Encoding.UTF8.GetString(authorBytes);

                titleBytes = System.Convert.FromBase64String(titleBase64);
                title = System.Text.Encoding.UTF8.GetString(titleBytes);

                path = $"storage/{authorBase64}/{titleBase64}.jpg";

                graphics.Add(new Graphic(author, title, titleBase64, path)); 

                if (graphics.Count >= limit)
                {
                    break;
                }
            }
            return graphics;
        }

        public IEnumerable<Graphic> GetUserImages(string author)
        {
            List<Graphic> graphics = new List<Graphic>();
            if(author != null)
            {
                byte[] authorBytes = System.Text.Encoding.UTF8.GetBytes(author);
                string authorBase64 = System.Convert.ToBase64String(authorBytes);

                string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

                string[] files = Directory.GetFiles(currentDirectory + $"\\storage\\{authorBase64}\\", "*.*");

                byte[] titleBytes;
                string titleBase64;
                string title;

                string path;

                foreach (string file in files)
                {
                    titleBase64 = Path.GetFileNameWithoutExtension(file);
                    titleBytes = System.Convert.FromBase64String(titleBase64);
                    title = System.Text.Encoding.UTF8.GetString(titleBytes);
                    path = $"storage/{authorBase64}/{titleBase64}.jpg";
                    graphics.Add(new Graphic(author, title, titleBase64, path));
                }
            }

            return graphics;
        }
    }
}
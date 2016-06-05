﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
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
            byte[] authorBytes = System.Text.Encoding.UTF8.GetBytes(graphic.Author);
            string authorBase64 = System.Convert.ToBase64String(authorBytes);
            string path = $"storage/{authorBase64}/{graphic.Id}.jpg";

            Image img = Image.FromStream(new MemoryStream(graphic.Bytes));
            img.Save(path, ImageFormat.Jpeg);

            return path;
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

            string path = $"storage/{authorBase64}/{id}.jpg";

            if(File.Exists(path))
            {
                return new Graphic(author, title, id, path);
            }

            return null;
        }

        public IEnumerable<Graphic> GetNewestImages(int limit)
        {
            List<Graphic> graphics = new List<Graphic>();
            string[] files = Directory.GetFiles(@"storage/", "*.*", SearchOption.AllDirectories);

            string authorBase64;
            byte[] authorBytes;
            string author;
            string path;
            string titleBase64;
            byte[] titleBytes;
            string title;
            foreach (string file in files)
            {
                Match match = Regex.Match(file, @"/storage/([^/]+)/([^\.]+)$", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    authorBase64 = match.Groups[1].Value;
                    titleBase64 = match.Groups[2].Value;

                    authorBytes = System.Convert.FromBase64String(authorBase64);
                    author = System.Text.Encoding.UTF8.GetString(authorBytes);

                    titleBytes = System.Convert.FromBase64String(titleBase64);
                    title = System.Text.Encoding.UTF8.GetString(titleBytes);

                    path = $"storage/{authorBase64}/{titleBase64}.jpg";
                    graphics.Add(new Graphic(author, title, titleBase64, path));
                }

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

            byte[] authorBytes = System.Text.Encoding.UTF8.GetBytes(author);
            string authorBase64 = System.Convert.ToBase64String(authorBytes);

            string[] files = Directory.GetFiles($"storage/{authorBase64}/", "*.*");

            byte[] titleBytes;
            string titleBase64;
            string title;

            string path;

            foreach (string file in files)
            {
                Match match = Regex.Match(file, $"/storage/{authorBase64}/([^\\.]+)$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    titleBase64 = match.Groups[1].Value;
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
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace ImageService.Models
{
    [DataContract]
    public class Graphic
    {
        /// <summary>
        /// Image author ID.
        /// </summary>
        [DataMember]
        public int Author { get; private set; }

        /// <summary>
        /// Image ID.
        /// </summary>
        [DataMember]
        public string Id { get; private set; }

        /// <summary>
        /// Image title.
        /// </summary>
        [DataMember]
        public string Title { get; private set; }

        /// <summary>
        /// Path to the image.
        /// </summary>
        [DataMember]
        public string Path { get; set; }

        /// <summary>
        /// Image object parsed from provided stream.
        /// </summary>
        [DataMember]
        public Image Image { get; set; }

        /// <summary>
        /// Creates graphic model based on user ID, image title and stream.
        /// ID is generated from image title.
        /// Path property is not set.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="stream"></param>
        public Graphic(int author, string title, Stream stream)
        {
            this.Image = Image.FromStream(stream);
            this.Author = author;
            this.Title = title;
            this.Id = this.generateId(title);
        }

        /// <summary>
        /// Creates graphic model based on author, title, id and path.
        /// Image property is not set.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public Graphic(int author, string title, string id, string path)
        {
            this.Author = author;
            this.Title = title;
            this.Id = id;
            this.Path = path;
        }

        /// <summary>
        /// Generates base64 image ID of given title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        protected string generateId(string title)
        {
            byte[] titleBytes = System.Text.Encoding.UTF8.GetBytes(title);

            return System.Convert.ToBase64String(titleBytes);
        }
    }
}

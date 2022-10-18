using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
        public class FileAttachment
        {
        public FileAttachment()
        {

        }
        public FileAttachment(string fileName,byte[] data, string contentType)
        {
            this.FileName = fileName;
            this.Data = data;
            this.ContentType = contentType;
        }
            public string FileName { get; set; }
            public byte[] Data { get; set; }
            public string ContentType { get; set; }
        }
}

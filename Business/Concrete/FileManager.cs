using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public string FileSave(IFormFile file, string filePath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            string path = filePath+ fileName;
            using var stream = System.IO.File.Create(path);
            file.CopyTo(stream);

            return fileName;
        }

        public byte[] FileConvertByteArrayToDatabase(IFormFile file)
        {
            using MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string fileToString = Convert.ToBase64String(fileBytes);

            return fileBytes;
        }
    }
}

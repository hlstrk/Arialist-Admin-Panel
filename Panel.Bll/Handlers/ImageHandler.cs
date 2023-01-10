using Panel.Bll.Utils;
using Panel.Interfaces;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Bll.Handlers
{
    public interface IImageHandler
    {
        Task<string> UploadImage(IFormFile file, string PictureType);

    }


    public class ImageHandler : IImageHandler
    {

        private readonly IImageWriter _imageWriter;

        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }


        public async Task<string> UploadImage(IFormFile file, string PictureType)
        {

            var result = await _imageWriter.UploadImage(file, PictureType);
            return result;
        }


    }
}


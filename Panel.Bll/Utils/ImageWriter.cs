using Panel.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Bll.Utils
{
    public class ImageWriter : IImageWriter
    {

        private async Task<string> WriteFile(IFormFile file, string PictureType = "")
        {

            string fileName;
            try
            {
                var extension = new StringBuilder(".")
                    .Append(file.FileName.Split(".")[file.FileName.Split('.').Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension)
                    .ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Pictures", fileName);
                if (PictureType == "ProfilePic")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Pictures\\ProfilePictures", fileName);
                }
                else if (PictureType == "SponsoredPic")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Pictures\\SponsoredPictures", fileName);
                }
                else if (PictureType == "PostPic")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\Pictures\\PostedPictures", fileName);
                }

                using (var bits = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(bits);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fileName;

        }





        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();

            }
            return WriteHelper.GetImageFormat(fileBytes) != WriteHelper.ImageFormat.unknown;
        }


        public async Task<string> UploadImage(IFormFile file, string PictureType = "")
        {
            if (CheckIfImageFile(file))

            {
                return await WriteFile(file, PictureType);

            }
            return " Invalid image file ";

        }


    }

}


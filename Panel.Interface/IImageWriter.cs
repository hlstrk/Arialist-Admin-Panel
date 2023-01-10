
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Interfaces
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file, string PictureType);
    }


}

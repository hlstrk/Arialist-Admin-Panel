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
    public interface IVideoHandler
    {
        Task<string> UploadVideo(IFormFile file, string VideoType);

    }


    public class VideoHandler : IVideoHandler
    {

        private readonly IVideoWriter _videoWriter;

        public VideoHandler(IVideoWriter videoWriter)
        {
            _videoWriter = videoWriter;
        }


        public async Task<string> UploadVideo(IFormFile file, string VideoType)
        {

            var result = await _videoWriter.UploadVideo(file, VideoType);
            return result;
        }


    }
}


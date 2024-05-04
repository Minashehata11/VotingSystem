using DAL.VotingSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Services
{
    public interface IFlaskConsumer
    {
        public Task<ExtractedData> DataExtractionAsync(IFormFile idCardImage);

        public  Task<dynamic> FaceRecognitionAsync(IFormFile idCardImage, IFormFile cameraImage);

    }
}

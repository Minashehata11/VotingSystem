using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

using DAL.VotingSystem.Entities;

namespace PL.VotingSystem.Controllers;

[ApiController]
[Route("[controller]")]

public class FlaskConsumer : ControllerBase
{
    //private readonly HttpClient _httpClient;

    //public FlaskConsumer()
    //{
    //    _httpClient = new HttpClient();
    //    // Set base URL of Flask API
    //    _httpClient.BaseAddress = new Uri("http://localhost:5000/micro_services/");
    //}

    //[HttpPost("FaceRecognition")]
    //public async Task<dynamic> FaceRecognitionAsync([FromForm] IFormFile idCardImage, [FromForm] IFormFile cameraImage)
    //{
    //    try
    //    {
    //        MultipartFormDataContent formData = new MultipartFormDataContent();
    //        formData.Add(new StreamContent(idCardImage.OpenReadStream()), "id_card_image", idCardImage.FileName);
    //        formData.Add(new StreamContent(cameraImage.OpenReadStream()), "camera_image", cameraImage.FileName);


    //        HttpResponseMessage response = await _httpClient.PostAsync("face_recognition", formData);
    //        response.EnsureSuccessStatusCode();

    //        dynamic jsonResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync())!;
    //        return jsonResponse;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        throw new Exception($"HTTP request failed: {ex.Message}");
    //    }
    //}
    //[HttpPost("DataExtraction")]
    //public async Task<ExtractedData> DataExtractionAsync([FromForm] IFormFile idCardImage)
    //{
    //    try
    //    {
    //        MultipartFormDataContent formData = new MultipartFormDataContent();
    //        formData.Add(new StreamContent(idCardImage.OpenReadStream()), "id_card_image", idCardImage.FileName);

    //        HttpResponseMessage response = await _httpClient.PostAsync("data_extraction", formData);
    //        response.EnsureSuccessStatusCode();

    //        ExtractedData extractedData = JsonSerializer.Deserialize<ExtractedData>(await response.Content.ReadAsStringAsync())!;

    //        return extractedData;

    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        throw new Exception($"HTTP request failed: {ex.Message}");
    //    }
    //}


}
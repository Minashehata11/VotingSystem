using DAL.VotingSystem.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BLL.VotingSystem.Services
{
    public class FlaskConsumer : IFlaskConsumer
    {
        private readonly HttpClient _httpClient;

        public FlaskConsumer()
        {
            _httpClient = new HttpClient();
            // Set base URL of Flask API
            _httpClient.BaseAddress = new Uri("http://localhost:5000/micro_services/");
        }

        public async Task<dynamic> FaceRecognitionAsync( IFormFile idCardImage, IFormFile cameraImage)
        {
            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(idCardImage.OpenReadStream()), "id_card_image", idCardImage.FileName);
                formData.Add(new StreamContent(cameraImage.OpenReadStream()), "camera_image", cameraImage.FileName);


                HttpResponseMessage response = await _httpClient.PostAsync("face_recognition", formData);
                response.EnsureSuccessStatusCode();

                dynamic jsonResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync())!;
                
                return jsonResponse;
              
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP request failed: {ex.Message}");
            }
        }
        public async Task<ExtractedData> DataExtractionAsync( IFormFile idCardImage)
        {
            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(idCardImage.OpenReadStream()), "id_card_image", idCardImage.FileName);

                HttpResponseMessage response = await _httpClient.PostAsync("data_extraction", formData);
                response.EnsureSuccessStatusCode();

                ExtractedData extractedData = JsonSerializer.Deserialize<ExtractedData>(await response.Content.ReadAsStringAsync())!;

                return extractedData;

            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP request failed: {ex.Message}");
            }
        }
    }
}

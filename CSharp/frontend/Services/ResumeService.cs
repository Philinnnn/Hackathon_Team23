using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.Services
{
    public class ResumeService
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "http://localhost:8080/api/search";

        public ResumeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Resume>> GetResumesAsync(ResumeQuery query)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(query, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                Console.WriteLine("Sending request to: POST /api/search");
                Console.WriteLine("Request content: " + jsonContent);

                using (var response = await _httpClient.PostAsync("http://localhost:8080/api/search", content))
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response code: " + response.StatusCode);
                    Console.WriteLine("Server response: " + responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonSerializer.Deserialize<List<Resume>>(responseContent) ?? new List<Resume>();
                    }
                    else
                    {
                        throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending request: " + ex.Message);
                throw new Exception("Failed to retrieve resumes. Please try again later.");
            }
        }
    }
}

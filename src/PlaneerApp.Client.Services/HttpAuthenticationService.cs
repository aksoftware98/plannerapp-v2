using PlaneerApp.Client.Services.Exceptions;
using PlaneerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PlaneerApp.Client.Services
{
    public class HttpAuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _client;

        public HttpAuthenticationService(HttpClient client)
        {
            _client = client; 
        }

        public async Task<ApiResponse> RegisterUserAsync(RegisterRequest model)
        {
            var response = await _client.PostAsJsonAsync("/api/v2/auth/register", model); 
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return result; 
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode); 
            }
        }
    }
}

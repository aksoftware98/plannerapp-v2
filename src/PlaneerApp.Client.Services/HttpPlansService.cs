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
    public class HttpPlansService : IPlansService
    {

        private readonly HttpClient _httpClient;

        public HttpPlansService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<PlanDetail>> CreateAsync(PlanDetail model, FormFile coverFile)
        {
            var form = PreparePlanForm(model, coverFile, false);

            var response = await _httpClient.PostAsync("/api/v2/plans", form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<PlanDetail>> EditAsync(PlanDetail model, FormFile coverFile)
        {
            var form = PreparePlanForm(model, coverFile, true);

            var response = await _httpClient.PutAsync("/api/v2/plans", form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string query = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/v2/plans?query={query}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PlanSummary>>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<PlanDetail>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v2/plans/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }

        }

        public async Task DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/v2/plans/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        private HttpContent PreparePlanForm(PlanDetail model, FormFile coverFile, bool isUpdate)
        {
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(model.Title), nameof(PlanDetail.Title));
            if (!string.IsNullOrWhiteSpace(model.Description))
                form.Add(new StringContent(model.Description), nameof(PlanDetail.Description));
            if (isUpdate)
                form.Add(new StringContent(model.Id), nameof(PlanDetail.Id));
            if (coverFile != null)
                form.Add(new StreamContent(coverFile.FileStream), nameof(model.CoverFile), coverFile.FileName);

            return form;
        }

    }
}

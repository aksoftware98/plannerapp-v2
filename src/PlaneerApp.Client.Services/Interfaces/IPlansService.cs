using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneerApp.Client.Services.Interfaces
{
    public interface IPlansService
    {

        Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string query = null, int pageNumber = 1, int pageSize = 10);

        Task<ApiResponse<PlanDetail>> CreateAsync(PlanDetail model, FormFile coverFile); 

        Task<ApiResponse<PlanDetail>> EditAsync(PlanDetail model, FormFile coverFile); 

    }
}

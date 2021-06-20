using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Value { get; set; }
    }

}

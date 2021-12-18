using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PlannerApp.Shared.Models
{
    public class PlanDetail : PlanSummary
    {

        public IFormFile CoverFile { get; set; }

        // List of the to-dos 
        public List<ToDoItemDetail> ToDoItems { get; set; }
    }

}

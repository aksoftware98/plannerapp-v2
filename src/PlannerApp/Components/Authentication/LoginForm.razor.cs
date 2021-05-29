using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class LoginForm : ComponentBase
    {

        [Inject]
        public HttpClient HttpClient { get; set; }

        private LoginRequest _model = new LoginRequest(); 

        private async Task LoginUserAsync()
        {
            throw new NotImplementedException();
        }

    }
}

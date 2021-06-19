using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using PlannerApp;
using PlannerApp.Shared;
using PlannerApp.Components;
using MudBlazor;
using Blazored.FluentValidation;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components
{
    public partial class PlanCard
    {

        [Parameter]
        public PlanSummary PlanSummary { get; set; } 

        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnEditClicked { get; set; }

    }
}
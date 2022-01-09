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
using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Blazor;

namespace PlannerApp.Pages.Plans
{
    public partial class CreateEditPlan
    {

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public ILanguageContainerService Language { get; set; }

        protected override void OnInitialized()
        {
            Language.InitLocalizedComponent(this);
        }

        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem(Language["Home"], "/index"),
            new BreadcrumbItem(Language["Plans"], "/plans"),
            new BreadcrumbItem(Language["Form"], "/plans/form", true)
        };

    }
}
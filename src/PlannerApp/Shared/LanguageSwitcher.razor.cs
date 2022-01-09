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
using Blazored.LocalStorage;
using System.Globalization;

namespace PlannerApp.Shared
{
    public partial class LanguageSwitcher
    {

        [Inject]
        public ILanguageContainerService Language { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("language"))
            {
                string cultureCode = await LocalStorage.GetItemAsStringAsync("language");
                Language.SetLanguage(CultureInfo.GetCultureInfo(cultureCode));
            }
        }

        private async Task ChangeLanguageAsync(string cultureCode)
        {
            Language.SetLanguage(CultureInfo.GetCultureInfo(cultureCode));

            await LocalStorage.SetItemAsStringAsync("language", cultureCode);
        }

    }
}
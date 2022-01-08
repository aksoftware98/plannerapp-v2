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
using PlaneerApp.Client.Services.Interfaces;
using PlaneerApp.Client.Services.Exceptions;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components
{
    public partial class CreateToDoItemForm
    {

        [Inject]
        public IToDoItemsService ToDoItemsService { get; set; }

        [Parameter]
        public string PlanId { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnToDoItemAdded { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false; 
        private string _description { get; set; }
        private string _errorMessage = string.Empty;

        private async Task AddToItemAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    _errorMessage = "Description is required";
                    return;
                }

                _isBusy = true;
                // Call the API to add the item 
                var result = await ToDoItemsService.CreateAsync(_description, PlanId);
                _description = String.Empty;

                // Notify the parent about the newly added item 
                await OnToDoItemAdded.InvokeAsync(result.Value);
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }
            _isBusy = false;
        }



    }
}
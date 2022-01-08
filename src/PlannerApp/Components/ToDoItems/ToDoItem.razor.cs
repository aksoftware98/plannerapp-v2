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
using PlaneerApp.Client.Services.Interfaces;
using PlaneerApp.Client.Services.Exceptions;

namespace PlannerApp.Components
{
    public partial class ToDoItem
    {
        [Inject]
        public IToDoItemsService ToDoItemsService { get; set; }

        [Parameter]
        public ToDoItemDetail Item { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnItemDeleted { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnItemEdited { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isChecked = true;

        private bool _isEditMode = false;
        private bool _isBusy = false;
        private string _description = String.Empty;
        private string _errorMessage = string.Empty;
        private string _descriptionStyle => $"cursor:pointer;{(!_isChecked ? "" : "text-decoration: line-through")}";

        protected override void OnInitialized()
        {
            _isChecked = Item.IsDone;
        }

        private void ToggleEditMode(bool isCancel)
        {
            if (_isEditMode)
            {
                _isEditMode = false;
                _description = isCancel ? Item.Description : _description;
            }
            else
            {
                _isEditMode = true;
                _description = Item.Description;
            }
          
        }


        private async Task RemoveItemAsync()
        {
            try
            {
                _isBusy = true;
                // Call the API to add the item 
                await ToDoItemsService.DeleteAsync(Item.Id);
              
                // Notify the parent about the newly added item 
                await OnItemDeleted.InvokeAsync(Item);
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }
            _isBusy = false;
        }

        private async Task EditItemAsync()
        {
            _errorMessage = String.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    _errorMessage = "Description is required";
                    return; 
                }

                _isBusy = true;
                // Call the API to add the item 
                var result = await ToDoItemsService.EditAsync(Item.Id, _description, Item.PlanId);
                ToggleEditMode(false);

                // Notify the parent about the newly added item 
                await OnItemEdited.InvokeAsync(result.Value);
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

        private async Task ToggleItemAsync(bool value)
        {
            _errorMessage = String.Empty;
            try
            {
                _isBusy = true;
                // Call the API to add the item 
                await ToDoItemsService.ToggleAsync(Item.Id);
                Item.IsDone = !Item.IsDone;
                _isChecked = Item.IsDone; 

                // Notify the parent about the newly added item 
                await OnItemEdited.InvokeAsync(Item);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace LibraryManagementSystem.Client.Pages
{
    public partial class EditLibraryEmployee
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public MyLibraryDBService MyLibraryDBService { get; set; }

        [Parameter]
        public long LibraryEmployeeID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            libraryEmployee = await MyLibraryDBService.GetLibraryEmployeeByLibraryEmployeeId(libraryEmployeeId:LibraryEmployeeID);
        }
        protected bool errorVisible;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryEmployee;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await MyLibraryDBService.UpdateLibraryEmployee(libraryEmployeeId:LibraryEmployeeID, libraryEmployee);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(libraryEmployee);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            libraryEmployee = await MyLibraryDBService.GetLibraryEmployeeByLibraryEmployeeId(libraryEmployeeId:LibraryEmployeeID);
        }
    }
}
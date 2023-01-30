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
    public partial class EditBookDetail
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
        public long BookID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bookDetail = await MyLibraryDBService.GetBookDetailByBookId(bookId:BookID);
        }
        protected bool errorVisible;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookDetail;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> bindingDetailsForBindingID;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> categoryDetailsForCategoryID;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> bookShelvesForShelfID;


        protected int bindingDetailsForBindingIDCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail bindingDetailsForBindingIDValue;
        protected async Task bindingDetailsForBindingIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetBindingDetails();
                bindingDetailsForBindingID = result.Value.AsODataEnumerable();
                bindingDetailsForBindingIDCount = bindingDetailsForBindingID.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }

        protected int categoryDetailsForCategoryIDCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail categoryDetailsForCategoryIDValue;
        protected async Task categoryDetailsForCategoryIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetCategoryDetails();
                categoryDetailsForCategoryID = result.Value.AsODataEnumerable();
                categoryDetailsForCategoryIDCount = categoryDetailsForCategoryID.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }

        protected int bookShelvesForShelfIDCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf bookShelvesForShelfIDValue;
        protected async Task bookShelvesForShelfIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetBookShelves();
                bookShelvesForShelfID = result.Value.AsODataEnumerable();
                bookShelvesForShelfIDCount = bookShelvesForShelfID.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await MyLibraryDBService.UpdateBookDetail(bookId:BookID, bookDetail);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(bookDetail);
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

            bookDetail = await MyLibraryDBService.GetBookDetailByBookId(bookId:BookID);
        }
    }
}
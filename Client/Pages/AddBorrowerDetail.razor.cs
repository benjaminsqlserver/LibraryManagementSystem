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
    public partial class AddBorrowerDetail
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

        protected override async Task OnInitializedAsync()
        {
            borrowerDetail = new LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail();
        }
        protected bool errorVisible;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail borrowerDetail;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> bookDetailsForBookID;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> libraryClientsForBorrowedBy;

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> libraryEmployeesForIssuedBy;


        protected int bookDetailsForBookIDCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookDetailsForBookIDValue;
        protected async Task bookDetailsForBookIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetBookDetails();
                bookDetailsForBookID = result.Value.AsODataEnumerable();
                bookDetailsForBookIDCount = bookDetailsForBookID.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }

        protected int libraryClientsForBorrowedByCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryClientsForBorrowedByValue;
        protected async Task libraryClientsForBorrowedByLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetLibraryClients();
                libraryClientsForBorrowedBy = result.Value.AsODataEnumerable();
                libraryClientsForBorrowedByCount = libraryClientsForBorrowedBy.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }

        protected int libraryEmployeesForIssuedByCount;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryEmployeesForIssuedByValue;
        protected async Task libraryEmployeesForIssuedByLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetLibraryEmployees();
                libraryEmployeesForIssuedBy = result.Value.AsODataEnumerable();
                libraryEmployeesForIssuedByCount = libraryEmployeesForIssuedBy.Count();

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
                var result = await MyLibraryDBService.CreateBorrowerDetail(borrowerDetail);
                DialogService.Close(borrowerDetail);
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
    }
}
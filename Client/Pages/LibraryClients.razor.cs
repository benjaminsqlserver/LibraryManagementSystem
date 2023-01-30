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
    public partial class LibraryClients
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

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> libraryClients;

        protected RadzenDataGrid<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> grid0;
        protected int count;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await MyLibraryDBService.GetLibraryClients(filter: $@"(contains(FirstName,""{search}"") or contains(LastName,""{search}"") or contains(EmailAddress,""{search}"") or contains(Password,""{search}"") or contains(ConfirmPassword,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                libraryClients = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load LibraryClients" });
            }
        }    

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddLibraryClient>("Add LibraryClient", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> args)
        {
            await DialogService.OpenAsync<EditLibraryClient>("Edit LibraryClient", new Dictionary<string, object> { {"LibraryClientID", args.Data.LibraryClientID} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryClient)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await MyLibraryDBService.DeleteLibraryClient(libraryClientId:libraryClient.LibraryClientID);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete LibraryClient" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await MyLibraryDBService.ExportLibraryClientsToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "LibraryClients");
            }

            if (args == null || args.Value == "xlsx")
            {
                await MyLibraryDBService.ExportLibraryClientsToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "LibraryClients");
            }
        }
    }
}
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
    public partial class BookDetails
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

        protected IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> bookDetails;

        protected RadzenDataGrid<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> grid0;
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
                var result = await MyLibraryDBService.GetBookDetails(filter: $@"(contains(ISBN,""{search}"") or contains(Language,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "BindingDetail,CategoryDetail,BookShelf", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                bookDetails = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load BookDetails" });
            }
        }    

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddBookDetail>("Add BookDetail", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> args)
        {
            await DialogService.OpenAsync<EditBookDetail>("Edit BookDetail", new Dictionary<string, object> { {"BookID", args.Data.BookID} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookDetail)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await MyLibraryDBService.DeleteBookDetail(bookId:bookDetail.BookID);

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
                    Detail = $"Unable to delete BookDetail" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await MyLibraryDBService.ExportBookDetailsToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "BindingDetail,CategoryDetail,BookShelf", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "BookDetails");
            }

            if (args == null || args.Value == "xlsx")
            {
                await MyLibraryDBService.ExportBookDetailsToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "BindingDetail,CategoryDetail,BookShelf", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "BookDetails");
            }
        }
    }
}
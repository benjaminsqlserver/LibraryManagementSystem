using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using LibraryManagementSystem.Server.Data;

namespace LibraryManagementSystem.Server.Controllers
{
    public partial class ExportMyLibraryDBController : ExportController
    {
        private readonly MyLibraryDBContext context;
        private readonly MyLibraryDBService service;

        public ExportMyLibraryDBController(MyLibraryDBContext context, MyLibraryDBService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/MyLibraryDB/bindingdetails/csv")]
        [HttpGet("/export/MyLibraryDB/bindingdetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBindingDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBindingDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/bindingdetails/excel")]
        [HttpGet("/export/MyLibraryDB/bindingdetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBindingDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBindingDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/bookdetails/csv")]
        [HttpGet("/export/MyLibraryDB/bookdetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBookDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/bookdetails/excel")]
        [HttpGet("/export/MyLibraryDB/bookdetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBookDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/bookshelves/csv")]
        [HttpGet("/export/MyLibraryDB/bookshelves/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookShelvesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBookShelves(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/bookshelves/excel")]
        [HttpGet("/export/MyLibraryDB/bookshelves/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookShelvesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBookShelves(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/borrowerdetails/csv")]
        [HttpGet("/export/MyLibraryDB/borrowerdetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBorrowerDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBorrowerDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/borrowerdetails/excel")]
        [HttpGet("/export/MyLibraryDB/borrowerdetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBorrowerDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBorrowerDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/categorydetails/csv")]
        [HttpGet("/export/MyLibraryDB/categorydetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoryDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategoryDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/categorydetails/excel")]
        [HttpGet("/export/MyLibraryDB/categorydetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoryDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategoryDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/libraryclients/csv")]
        [HttpGet("/export/MyLibraryDB/libraryclients/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLibraryClientsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLibraryClients(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/libraryclients/excel")]
        [HttpGet("/export/MyLibraryDB/libraryclients/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLibraryClientsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLibraryClients(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/libraryemployees/csv")]
        [HttpGet("/export/MyLibraryDB/libraryemployees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLibraryEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLibraryEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/MyLibraryDB/libraryemployees/excel")]
        [HttpGet("/export/MyLibraryDB/libraryemployees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLibraryEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLibraryEmployees(), Request.Query), fileName);
        }
    }
}

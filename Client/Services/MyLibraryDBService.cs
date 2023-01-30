
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace LibraryManagementSystem.Client
{
    public partial class MyLibraryDBService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public MyLibraryDBService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/MyLibraryDB/");
        }


        public async System.Threading.Tasks.Task ExportBindingDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bindingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bindingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBindingDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bindingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bindingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBindingDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>> GetBindingDetails(Query query)
        {
            return await GetBindingDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>> GetBindingDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BindingDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBindingDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>>(response);
        }

        partial void OnCreateBindingDetail(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> CreateBindingDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail bindingDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail))
        {
            var uri = new Uri(baseUri, $"BindingDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bindingDetail), Encoding.UTF8, "application/json");

            OnCreateBindingDetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>(response);
        }

        partial void OnDeleteBindingDetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBindingDetail(int bindingId = default(int))
        {
            var uri = new Uri(baseUri, $"BindingDetails({bindingId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBindingDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBindingDetailByBindingId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> GetBindingDetailByBindingId(string expand = default(string), int bindingId = default(int))
        {
            var uri = new Uri(baseUri, $"BindingDetails({bindingId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBindingDetailByBindingId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>(response);
        }

        partial void OnUpdateBindingDetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBindingDetail(int bindingId = default(int), LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail bindingDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail))
        {
            var uri = new Uri(baseUri, $"BindingDetails({bindingId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bindingDetail.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bindingDetail), Encoding.UTF8, "application/json");

            OnUpdateBindingDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBookDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBookDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBookDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>> GetBookDetails(Query query)
        {
            return await GetBookDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>> GetBookDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BookDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>>(response);
        }

        partial void OnCreateBookDetail(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> CreateBookDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail))
        {
            var uri = new Uri(baseUri, $"BookDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookDetail), Encoding.UTF8, "application/json");

            OnCreateBookDetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>(response);
        }

        partial void OnDeleteBookDetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBookDetail(long bookId = default(long))
        {
            var uri = new Uri(baseUri, $"BookDetails({bookId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBookDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBookDetailByBookId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> GetBookDetailByBookId(string expand = default(string), long bookId = default(long))
        {
            var uri = new Uri(baseUri, $"BookDetails({bookId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookDetailByBookId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>(response);
        }

        partial void OnUpdateBookDetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBookDetail(long bookId = default(long), LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail))
        {
            var uri = new Uri(baseUri, $"BookDetails({bookId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bookDetail.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookDetail), Encoding.UTF8, "application/json");

            OnUpdateBookDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBookShelvesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookshelves/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookshelves/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBookShelvesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookshelves/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookshelves/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBookShelves(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>> GetBookShelves(Query query)
        {
            return await GetBookShelves(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>> GetBookShelves(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BookShelves");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookShelves(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>>(response);
        }

        partial void OnCreateBookShelf(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> CreateBookShelf(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf bookShelf = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf))
        {
            var uri = new Uri(baseUri, $"BookShelves");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookShelf), Encoding.UTF8, "application/json");

            OnCreateBookShelf(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>(response);
        }

        partial void OnDeleteBookShelf(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBookShelf(int shelfId = default(int))
        {
            var uri = new Uri(baseUri, $"BookShelves({shelfId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBookShelf(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBookShelfByShelfId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> GetBookShelfByShelfId(string expand = default(string), int shelfId = default(int))
        {
            var uri = new Uri(baseUri, $"BookShelves({shelfId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookShelfByShelfId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>(response);
        }

        partial void OnUpdateBookShelf(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBookShelf(int shelfId = default(int), LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf bookShelf = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf))
        {
            var uri = new Uri(baseUri, $"BookShelves({shelfId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", bookShelf.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookShelf), Encoding.UTF8, "application/json");

            OnUpdateBookShelf(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBorrowerDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/borrowerdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/borrowerdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBorrowerDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/borrowerdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/borrowerdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBorrowerDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>> GetBorrowerDetails(Query query)
        {
            return await GetBorrowerDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>> GetBorrowerDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BorrowerDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBorrowerDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>>(response);
        }

        partial void OnCreateBorrowerDetail(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> CreateBorrowerDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail borrowerDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail))
        {
            var uri = new Uri(baseUri, $"BorrowerDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(borrowerDetail), Encoding.UTF8, "application/json");

            OnCreateBorrowerDetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>(response);
        }

        partial void OnDeleteBorrowerDetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBorrowerDetail(long id = default(long))
        {
            var uri = new Uri(baseUri, $"BorrowerDetails({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBorrowerDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBorrowerDetailById(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> GetBorrowerDetailById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"BorrowerDetails({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBorrowerDetailById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>(response);
        }

        partial void OnUpdateBorrowerDetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBorrowerDetail(long id = default(long), LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail borrowerDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail))
        {
            var uri = new Uri(baseUri, $"BorrowerDetails({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", borrowerDetail.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(borrowerDetail), Encoding.UTF8, "application/json");

            OnUpdateBorrowerDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCategoryDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/categorydetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/categorydetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCategoryDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/categorydetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/categorydetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCategoryDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>> GetCategoryDetails(Query query)
        {
            return await GetCategoryDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>> GetCategoryDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"CategoryDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoryDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>>(response);
        }

        partial void OnCreateCategoryDetail(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> CreateCategoryDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail categoryDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail))
        {
            var uri = new Uri(baseUri, $"CategoryDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoryDetail), Encoding.UTF8, "application/json");

            OnCreateCategoryDetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>(response);
        }

        partial void OnDeleteCategoryDetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCategoryDetail(int categoryId = default(int))
        {
            var uri = new Uri(baseUri, $"CategoryDetails({categoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCategoryDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCategoryDetailByCategoryId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> GetCategoryDetailByCategoryId(string expand = default(string), int categoryId = default(int))
        {
            var uri = new Uri(baseUri, $"CategoryDetails({categoryId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoryDetailByCategoryId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>(response);
        }

        partial void OnUpdateCategoryDetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCategoryDetail(int categoryId = default(int), LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail categoryDetail = default(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail))
        {
            var uri = new Uri(baseUri, $"CategoryDetails({categoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", categoryDetail.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(categoryDetail), Encoding.UTF8, "application/json");

            OnUpdateCategoryDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLibraryClientsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryclients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryclients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLibraryClientsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryclients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryclients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLibraryClients(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>> GetLibraryClients(Query query)
        {
            return await GetLibraryClients(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>> GetLibraryClients(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"LibraryClients");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLibraryClients(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>>(response);
        }

        partial void OnCreateLibraryClient(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> CreateLibraryClient(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryClient = default(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient))
        {
            var uri = new Uri(baseUri, $"LibraryClients");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(libraryClient), Encoding.UTF8, "application/json");

            OnCreateLibraryClient(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>(response);
        }

        partial void OnDeleteLibraryClient(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLibraryClient(long libraryClientId = default(long))
        {
            var uri = new Uri(baseUri, $"LibraryClients({libraryClientId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLibraryClient(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLibraryClientByLibraryClientId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> GetLibraryClientByLibraryClientId(string expand = default(string), long libraryClientId = default(long))
        {
            var uri = new Uri(baseUri, $"LibraryClients({libraryClientId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLibraryClientByLibraryClientId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>(response);
        }

        partial void OnUpdateLibraryClient(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLibraryClient(long libraryClientId = default(long), LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryClient = default(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient))
        {
            var uri = new Uri(baseUri, $"LibraryClients({libraryClientId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", libraryClient.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(libraryClient), Encoding.UTF8, "application/json");

            OnUpdateLibraryClient(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLibraryEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLibraryEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLibraryEmployees(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>> GetLibraryEmployees(Query query)
        {
            return await GetLibraryEmployees(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>> GetLibraryEmployees(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"LibraryEmployees");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLibraryEmployees(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>>(response);
        }

        partial void OnCreateLibraryEmployee(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> CreateLibraryEmployee(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryEmployee = default(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee))
        {
            var uri = new Uri(baseUri, $"LibraryEmployees");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(libraryEmployee), Encoding.UTF8, "application/json");

            OnCreateLibraryEmployee(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>(response);
        }

        partial void OnDeleteLibraryEmployee(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLibraryEmployee(long libraryEmployeeId = default(long))
        {
            var uri = new Uri(baseUri, $"LibraryEmployees({libraryEmployeeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLibraryEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLibraryEmployeeByLibraryEmployeeId(HttpRequestMessage requestMessage);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> GetLibraryEmployeeByLibraryEmployeeId(string expand = default(string), long libraryEmployeeId = default(long))
        {
            var uri = new Uri(baseUri, $"LibraryEmployees({libraryEmployeeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLibraryEmployeeByLibraryEmployeeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>(response);
        }

        partial void OnUpdateLibraryEmployee(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLibraryEmployee(long libraryEmployeeId = default(long), LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryEmployee = default(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee))
        {
            var uri = new Uri(baseUri, $"LibraryEmployees({libraryEmployeeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", libraryEmployee.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(libraryEmployee), Encoding.UTF8, "application/json");

            OnUpdateLibraryEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using LibraryManagementSystem.Server.Data;

namespace LibraryManagementSystem.Server
{
    public partial class MyLibraryDBService
    {
        MyLibraryDBContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly MyLibraryDBContext context;
        private readonly NavigationManager navigationManager;

        public MyLibraryDBService(MyLibraryDBContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        public async Task ExportBindingDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bindingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bindingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBindingDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bindingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bindingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBindingDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>> GetBindingDetails(Query query = null)
        {
            var items = Context.BindingDetails.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBindingDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBindingDetailGet(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> GetBindingDetailByBindingId(int bindingid)
        {
            var items = Context.BindingDetails
                              .AsNoTracking()
                              .Where(i => i.BindingID == bindingid);

  
            var itemToReturn = items.FirstOrDefault();

            OnBindingDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBindingDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> CreateBindingDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail bindingdetail)
        {
            OnBindingDetailCreated(bindingdetail);

            var existingItem = Context.BindingDetails
                              .Where(i => i.BindingID == bindingdetail.BindingID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BindingDetails.Add(bindingdetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bindingdetail).State = EntityState.Detached;
                throw;
            }

            OnAfterBindingDetailCreated(bindingdetail);

            return bindingdetail;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> CancelBindingDetailChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBindingDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> UpdateBindingDetail(int bindingid, LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail bindingdetail)
        {
            OnBindingDetailUpdated(bindingdetail);

            var itemToUpdate = Context.BindingDetails
                              .Where(i => i.BindingID == bindingdetail.BindingID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bindingdetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBindingDetailUpdated(bindingdetail);

            return bindingdetail;
        }

        partial void OnBindingDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> DeleteBindingDetail(int bindingid)
        {
            var itemToDelete = Context.BindingDetails
                              .Where(i => i.BindingID == bindingid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBindingDetailDeleted(itemToDelete);


            Context.BindingDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBindingDetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBookDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBookDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBookDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>> GetBookDetails(Query query = null)
        {
            var items = Context.BookDetails.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBookDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBookDetailGet(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> GetBookDetailByBookId(long bookid)
        {
            var items = Context.BookDetails
                              .AsNoTracking()
                              .Where(i => i.BookID == bookid);

                items = items.Include(i => i.BindingDetail);
                items = items.Include(i => i.CategoryDetail);
                items = items.Include(i => i.BookShelf);
  
            var itemToReturn = items.FirstOrDefault();

            OnBookDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBookDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> CreateBookDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookdetail)
        {
            OnBookDetailCreated(bookdetail);

            var existingItem = Context.BookDetails
                              .Where(i => i.BookID == bookdetail.BookID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BookDetails.Add(bookdetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bookdetail).State = EntityState.Detached;
                throw;
            }

            OnAfterBookDetailCreated(bookdetail);

            return bookdetail;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> CancelBookDetailChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBookDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> UpdateBookDetail(long bookid, LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail bookdetail)
        {
            OnBookDetailUpdated(bookdetail);

            var itemToUpdate = Context.BookDetails
                              .Where(i => i.BookID == bookdetail.BookID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bookdetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBookDetailUpdated(bookdetail);

            return bookdetail;
        }

        partial void OnBookDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> DeleteBookDetail(long bookid)
        {
            var itemToDelete = Context.BookDetails
                              .Where(i => i.BookID == bookid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBookDetailDeleted(itemToDelete);


            Context.BookDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBookDetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBookShelvesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookshelves/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookshelves/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBookShelvesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/bookshelves/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/bookshelves/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBookShelvesRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>> GetBookShelves(Query query = null)
        {
            var items = Context.BookShelves.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBookShelvesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBookShelfGet(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> GetBookShelfByShelfId(int shelfid)
        {
            var items = Context.BookShelves
                              .AsNoTracking()
                              .Where(i => i.ShelfID == shelfid);

  
            var itemToReturn = items.FirstOrDefault();

            OnBookShelfGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBookShelfCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> CreateBookShelf(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf bookshelf)
        {
            OnBookShelfCreated(bookshelf);

            var existingItem = Context.BookShelves
                              .Where(i => i.ShelfID == bookshelf.ShelfID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BookShelves.Add(bookshelf);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bookshelf).State = EntityState.Detached;
                throw;
            }

            OnAfterBookShelfCreated(bookshelf);

            return bookshelf;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> CancelBookShelfChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBookShelfUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> UpdateBookShelf(int shelfid, LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf bookshelf)
        {
            OnBookShelfUpdated(bookshelf);

            var itemToUpdate = Context.BookShelves
                              .Where(i => i.ShelfID == bookshelf.ShelfID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bookshelf);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBookShelfUpdated(bookshelf);

            return bookshelf;
        }

        partial void OnBookShelfDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> DeleteBookShelf(int shelfid)
        {
            var itemToDelete = Context.BookShelves
                              .Where(i => i.ShelfID == shelfid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBookShelfDeleted(itemToDelete);


            Context.BookShelves.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBookShelfDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBorrowerDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/borrowerdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/borrowerdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBorrowerDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/borrowerdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/borrowerdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBorrowerDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>> GetBorrowerDetails(Query query = null)
        {
            var items = Context.BorrowerDetails.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBorrowerDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBorrowerDetailGet(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> GetBorrowerDetailById(long id)
        {
            var items = Context.BorrowerDetails
                              .AsNoTracking()
                              .Where(i => i.ID == id);

                items = items.Include(i => i.BookDetail);
                items = items.Include(i => i.LibraryClient);
                items = items.Include(i => i.LibraryEmployee);
  
            var itemToReturn = items.FirstOrDefault();

            OnBorrowerDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBorrowerDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> CreateBorrowerDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail borrowerdetail)
        {
            OnBorrowerDetailCreated(borrowerdetail);

            var existingItem = Context.BorrowerDetails
                              .Where(i => i.ID == borrowerdetail.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BorrowerDetails.Add(borrowerdetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(borrowerdetail).State = EntityState.Detached;
                throw;
            }

            OnAfterBorrowerDetailCreated(borrowerdetail);

            return borrowerdetail;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> CancelBorrowerDetailChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBorrowerDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> UpdateBorrowerDetail(long id, LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail borrowerdetail)
        {
            OnBorrowerDetailUpdated(borrowerdetail);

            var itemToUpdate = Context.BorrowerDetails
                              .Where(i => i.ID == borrowerdetail.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(borrowerdetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBorrowerDetailUpdated(borrowerdetail);

            return borrowerdetail;
        }

        partial void OnBorrowerDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> DeleteBorrowerDetail(long id)
        {
            var itemToDelete = Context.BorrowerDetails
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBorrowerDetailDeleted(itemToDelete);


            Context.BorrowerDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBorrowerDetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCategoryDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/categorydetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/categorydetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoryDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/categorydetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/categorydetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoryDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>> GetCategoryDetails(Query query = null)
        {
            var items = Context.CategoryDetails.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCategoryDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoryDetailGet(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> GetCategoryDetailByCategoryId(int categoryid)
        {
            var items = Context.CategoryDetails
                              .AsNoTracking()
                              .Where(i => i.CategoryID == categoryid);

  
            var itemToReturn = items.FirstOrDefault();

            OnCategoryDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCategoryDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> CreateCategoryDetail(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail categorydetail)
        {
            OnCategoryDetailCreated(categorydetail);

            var existingItem = Context.CategoryDetails
                              .Where(i => i.CategoryID == categorydetail.CategoryID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CategoryDetails.Add(categorydetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(categorydetail).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoryDetailCreated(categorydetail);

            return categorydetail;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> CancelCategoryDetailChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoryDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> UpdateCategoryDetail(int categoryid, LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail categorydetail)
        {
            OnCategoryDetailUpdated(categorydetail);

            var itemToUpdate = Context.CategoryDetails
                              .Where(i => i.CategoryID == categorydetail.CategoryID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(categorydetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCategoryDetailUpdated(categorydetail);

            return categorydetail;
        }

        partial void OnCategoryDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> DeleteCategoryDetail(int categoryid)
        {
            var itemToDelete = Context.CategoryDetails
                              .Where(i => i.CategoryID == categoryid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoryDetailDeleted(itemToDelete);


            Context.CategoryDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoryDetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLibraryClientsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryclients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryclients/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLibraryClientsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryclients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryclients/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLibraryClientsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>> GetLibraryClients(Query query = null)
        {
            var items = Context.LibraryClients.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLibraryClientsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLibraryClientGet(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> GetLibraryClientByLibraryClientId(long libraryclientid)
        {
            var items = Context.LibraryClients
                              .AsNoTracking()
                              .Where(i => i.LibraryClientID == libraryclientid);

  
            var itemToReturn = items.FirstOrDefault();

            OnLibraryClientGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLibraryClientCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> CreateLibraryClient(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryclient)
        {
            OnLibraryClientCreated(libraryclient);

            var existingItem = Context.LibraryClients
                              .Where(i => i.LibraryClientID == libraryclient.LibraryClientID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LibraryClients.Add(libraryclient);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(libraryclient).State = EntityState.Detached;
                throw;
            }

            OnAfterLibraryClientCreated(libraryclient);

            return libraryclient;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> CancelLibraryClientChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLibraryClientUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> UpdateLibraryClient(long libraryclientid, LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryclient)
        {
            OnLibraryClientUpdated(libraryclient);

            var itemToUpdate = Context.LibraryClients
                              .Where(i => i.LibraryClientID == libraryclient.LibraryClientID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(libraryclient);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLibraryClientUpdated(libraryclient);

            return libraryclient;
        }

        partial void OnLibraryClientDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> DeleteLibraryClient(long libraryclientid)
        {
            var itemToDelete = Context.LibraryClients
                              .Where(i => i.LibraryClientID == libraryclientid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLibraryClientDeleted(itemToDelete);


            Context.LibraryClients.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLibraryClientDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLibraryEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLibraryEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/mylibrarydb/libraryemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/mylibrarydb/libraryemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLibraryEmployeesRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> items);

        public async Task<IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>> GetLibraryEmployees(Query query = null)
        {
            var items = Context.LibraryEmployees.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLibraryEmployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLibraryEmployeeGet(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> GetLibraryEmployeeByLibraryEmployeeId(long libraryemployeeid)
        {
            var items = Context.LibraryEmployees
                              .AsNoTracking()
                              .Where(i => i.LibraryEmployeeID == libraryemployeeid);

  
            var itemToReturn = items.FirstOrDefault();

            OnLibraryEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLibraryEmployeeCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> CreateLibraryEmployee(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryemployee)
        {
            OnLibraryEmployeeCreated(libraryemployee);

            var existingItem = Context.LibraryEmployees
                              .Where(i => i.LibraryEmployeeID == libraryemployee.LibraryEmployeeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LibraryEmployees.Add(libraryemployee);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(libraryemployee).State = EntityState.Detached;
                throw;
            }

            OnAfterLibraryEmployeeCreated(libraryemployee);

            return libraryemployee;
        }

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> CancelLibraryEmployeeChanges(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLibraryEmployeeUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> UpdateLibraryEmployee(long libraryemployeeid, LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryemployee)
        {
            OnLibraryEmployeeUpdated(libraryemployee);

            var itemToUpdate = Context.LibraryEmployees
                              .Where(i => i.LibraryEmployeeID == libraryemployee.LibraryEmployeeID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(libraryemployee);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLibraryEmployeeUpdated(libraryemployee);

            return libraryemployee;
        }

        partial void OnLibraryEmployeeDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        public async Task<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> DeleteLibraryEmployee(long libraryemployeeid)
        {
            var itemToDelete = Context.LibraryEmployees
                              .Where(i => i.LibraryEmployeeID == libraryemployeeid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLibraryEmployeeDeleted(itemToDelete);


            Context.LibraryEmployees.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLibraryEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}
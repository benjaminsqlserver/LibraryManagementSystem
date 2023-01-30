using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryManagementSystem.Server.Controllers.MyLibraryDB
{
    [Route("odata/MyLibraryDB/BookShelves")]
    public partial class BookShelvesController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public BookShelvesController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> GetBookShelves()
        {
            var items = this.context.BookShelves.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>();
            this.OnBookShelvesRead(ref items);

            return items;
        }

        partial void OnBookShelvesRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> items);

        partial void OnBookShelfGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/BookShelves(ShelfID={ShelfID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> GetBookShelf(int key)
        {
            var items = this.context.BookShelves.Where(i => i.ShelfID == key);
            var result = SingleResult.Create(items);

            OnBookShelfGet(ref result);

            return result;
        }
        partial void OnBookShelfDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        [HttpDelete("/odata/MyLibraryDB/BookShelves(ShelfID={ShelfID})")]
        public IActionResult DeleteBookShelf(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BookShelves
                    .Where(i => i.ShelfID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookShelfDeleted(item);
                this.context.BookShelves.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBookShelfDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookShelfUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        [HttpPut("/odata/MyLibraryDB/BookShelves(ShelfID={ShelfID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBookShelf(int key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookShelves
                    .Where(i => i.ShelfID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookShelfUpdated(item);
                this.context.BookShelves.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookShelves.Where(i => i.ShelfID == key);
                ;
                this.OnAfterBookShelfUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/BookShelves(ShelfID={ShelfID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBookShelf(int key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookShelves
                    .Where(i => i.ShelfID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBookShelfUpdated(item);
                this.context.BookShelves.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookShelves.Where(i => i.ShelfID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookShelfCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);
        partial void OnAfterBookShelfCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnBookShelfCreated(item);
                this.context.BookShelves.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookShelves.Where(i => i.ShelfID == item.ShelfID);

                ;

                this.OnAfterBookShelfCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}

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
    [Route("odata/MyLibraryDB/BookDetails")]
    public partial class BookDetailsController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public BookDetailsController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> GetBookDetails()
        {
            var items = this.context.BookDetails.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>();
            this.OnBookDetailsRead(ref items);

            return items;
        }

        partial void OnBookDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> items);

        partial void OnBookDetailGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/BookDetails(BookID={BookID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> GetBookDetail(long key)
        {
            var items = this.context.BookDetails.Where(i => i.BookID == key);
            var result = SingleResult.Create(items);

            OnBookDetailGet(ref result);

            return result;
        }
        partial void OnBookDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        [HttpDelete("/odata/MyLibraryDB/BookDetails(BookID={BookID})")]
        public IActionResult DeleteBookDetail(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BookDetails
                    .Where(i => i.BookID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookDetailDeleted(item);
                this.context.BookDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBookDetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        [HttpPut("/odata/MyLibraryDB/BookDetails(BookID={BookID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBookDetail(long key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookDetails
                    .Where(i => i.BookID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookDetailUpdated(item);
                this.context.BookDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookDetails.Where(i => i.BookID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BindingDetail,CategoryDetail,BookShelf");
                this.OnAfterBookDetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/BookDetails(BookID={BookID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBookDetail(long key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookDetails
                    .Where(i => i.BookID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBookDetailUpdated(item);
                this.context.BookDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookDetails.Where(i => i.BookID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BindingDetail,CategoryDetail,BookShelf");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);
        partial void OnAfterBookDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail item)
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

                this.OnBookDetailCreated(item);
                this.context.BookDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookDetails.Where(i => i.BookID == item.BookID);

                Request.QueryString = Request.QueryString.Add("$expand", "BindingDetail,CategoryDetail,BookShelf");

                this.OnAfterBookDetailCreated(item);

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

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
    [Route("odata/MyLibraryDB/CategoryDetails")]
    public partial class CategoryDetailsController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public CategoryDetailsController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> GetCategoryDetails()
        {
            var items = this.context.CategoryDetails.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>();
            this.OnCategoryDetailsRead(ref items);

            return items;
        }

        partial void OnCategoryDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> items);

        partial void OnCategoryDetailGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/CategoryDetails(CategoryID={CategoryID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> GetCategoryDetail(int key)
        {
            var items = this.context.CategoryDetails.Where(i => i.CategoryID == key);
            var result = SingleResult.Create(items);

            OnCategoryDetailGet(ref result);

            return result;
        }
        partial void OnCategoryDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        [HttpDelete("/odata/MyLibraryDB/CategoryDetails(CategoryID={CategoryID})")]
        public IActionResult DeleteCategoryDetail(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CategoryDetails
                    .Where(i => i.CategoryID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoryDetailDeleted(item);
                this.context.CategoryDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCategoryDetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoryDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        [HttpPut("/odata/MyLibraryDB/CategoryDetails(CategoryID={CategoryID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCategoryDetail(int key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoryDetails
                    .Where(i => i.CategoryID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoryDetailUpdated(item);
                this.context.CategoryDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoryDetails.Where(i => i.CategoryID == key);
                ;
                this.OnAfterCategoryDetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/CategoryDetails(CategoryID={CategoryID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCategoryDetail(int key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoryDetails
                    .Where(i => i.CategoryID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCategoryDetailUpdated(item);
                this.context.CategoryDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoryDetails.Where(i => i.CategoryID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoryDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);
        partial void OnAfterCategoryDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail item)
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

                this.OnCategoryDetailCreated(item);
                this.context.CategoryDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoryDetails.Where(i => i.CategoryID == item.CategoryID);

                ;

                this.OnAfterCategoryDetailCreated(item);

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

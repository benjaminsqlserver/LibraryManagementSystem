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
    [Route("odata/MyLibraryDB/BindingDetails")]
    public partial class BindingDetailsController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public BindingDetailsController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> GetBindingDetails()
        {
            var items = this.context.BindingDetails.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>();
            this.OnBindingDetailsRead(ref items);

            return items;
        }

        partial void OnBindingDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> items);

        partial void OnBindingDetailGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/BindingDetails(BindingID={BindingID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> GetBindingDetail(int key)
        {
            var items = this.context.BindingDetails.Where(i => i.BindingID == key);
            var result = SingleResult.Create(items);

            OnBindingDetailGet(ref result);

            return result;
        }
        partial void OnBindingDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        [HttpDelete("/odata/MyLibraryDB/BindingDetails(BindingID={BindingID})")]
        public IActionResult DeleteBindingDetail(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BindingDetails
                    .Where(i => i.BindingID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBindingDetailDeleted(item);
                this.context.BindingDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBindingDetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBindingDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        [HttpPut("/odata/MyLibraryDB/BindingDetails(BindingID={BindingID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBindingDetail(int key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BindingDetails
                    .Where(i => i.BindingID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBindingDetailUpdated(item);
                this.context.BindingDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BindingDetails.Where(i => i.BindingID == key);
                ;
                this.OnAfterBindingDetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/BindingDetails(BindingID={BindingID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBindingDetail(int key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BindingDetails
                    .Where(i => i.BindingID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBindingDetailUpdated(item);
                this.context.BindingDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BindingDetails.Where(i => i.BindingID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBindingDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);
        partial void OnAfterBindingDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail item)
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

                this.OnBindingDetailCreated(item);
                this.context.BindingDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BindingDetails.Where(i => i.BindingID == item.BindingID);

                ;

                this.OnAfterBindingDetailCreated(item);

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

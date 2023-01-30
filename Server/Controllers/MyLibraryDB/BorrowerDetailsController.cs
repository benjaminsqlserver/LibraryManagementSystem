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
    [Route("odata/MyLibraryDB/BorrowerDetails")]
    public partial class BorrowerDetailsController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public BorrowerDetailsController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> GetBorrowerDetails()
        {
            var items = this.context.BorrowerDetails.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>();
            this.OnBorrowerDetailsRead(ref items);

            return items;
        }

        partial void OnBorrowerDetailsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> items);

        partial void OnBorrowerDetailGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/BorrowerDetails(ID={ID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> GetBorrowerDetail(long key)
        {
            var items = this.context.BorrowerDetails.Where(i => i.ID == key);
            var result = SingleResult.Create(items);

            OnBorrowerDetailGet(ref result);

            return result;
        }
        partial void OnBorrowerDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        [HttpDelete("/odata/MyLibraryDB/BorrowerDetails(ID={ID})")]
        public IActionResult DeleteBorrowerDetail(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BorrowerDetails
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBorrowerDetailDeleted(item);
                this.context.BorrowerDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBorrowerDetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBorrowerDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        [HttpPut("/odata/MyLibraryDB/BorrowerDetails(ID={ID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBorrowerDetail(long key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BorrowerDetails
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBorrowerDetailUpdated(item);
                this.context.BorrowerDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BorrowerDetails.Where(i => i.ID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BookDetail,LibraryClient,LibraryEmployee");
                this.OnAfterBorrowerDetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/BorrowerDetails(ID={ID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBorrowerDetail(long key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BorrowerDetails
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBorrowerDetailUpdated(item);
                this.context.BorrowerDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BorrowerDetails.Where(i => i.ID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BookDetail,LibraryClient,LibraryEmployee");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBorrowerDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);
        partial void OnAfterBorrowerDetailCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail item)
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

                this.OnBorrowerDetailCreated(item);
                this.context.BorrowerDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BorrowerDetails.Where(i => i.ID == item.ID);

                Request.QueryString = Request.QueryString.Add("$expand", "BookDetail,LibraryClient,LibraryEmployee");

                this.OnAfterBorrowerDetailCreated(item);

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

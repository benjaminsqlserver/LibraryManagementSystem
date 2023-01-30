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
    [Route("odata/MyLibraryDB/LibraryClients")]
    public partial class LibraryClientsController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public LibraryClientsController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> GetLibraryClients()
        {
            var items = this.context.LibraryClients.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>();
            this.OnLibraryClientsRead(ref items);

            return items;
        }

        partial void OnLibraryClientsRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> items);

        partial void OnLibraryClientGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/LibraryClients(LibraryClientID={LibraryClientID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> GetLibraryClient(long key)
        {
            var items = this.context.LibraryClients.Where(i => i.LibraryClientID == key);
            var result = SingleResult.Create(items);

            OnLibraryClientGet(ref result);

            return result;
        }
        partial void OnLibraryClientDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        [HttpDelete("/odata/MyLibraryDB/LibraryClients(LibraryClientID={LibraryClientID})")]
        public IActionResult DeleteLibraryClient(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.LibraryClients
                    .Where(i => i.LibraryClientID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLibraryClientDeleted(item);
                this.context.LibraryClients.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLibraryClientDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLibraryClientUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        [HttpPut("/odata/MyLibraryDB/LibraryClients(LibraryClientID={LibraryClientID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLibraryClient(long key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LibraryClients
                    .Where(i => i.LibraryClientID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLibraryClientUpdated(item);
                this.context.LibraryClients.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryClients.Where(i => i.LibraryClientID == key);
                ;
                this.OnAfterLibraryClientUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/LibraryClients(LibraryClientID={LibraryClientID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLibraryClient(long key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LibraryClients
                    .Where(i => i.LibraryClientID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLibraryClientUpdated(item);
                this.context.LibraryClients.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryClients.Where(i => i.LibraryClientID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLibraryClientCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);
        partial void OnAfterLibraryClientCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient item)
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

                this.OnLibraryClientCreated(item);
                this.context.LibraryClients.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryClients.Where(i => i.LibraryClientID == item.LibraryClientID);

                ;

                this.OnAfterLibraryClientCreated(item);

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

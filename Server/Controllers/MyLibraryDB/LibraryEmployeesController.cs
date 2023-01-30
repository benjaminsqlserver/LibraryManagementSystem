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
    [Route("odata/MyLibraryDB/LibraryEmployees")]
    public partial class LibraryEmployeesController : ODataController
    {
        private LibraryManagementSystem.Server.Data.MyLibraryDBContext context;

        public LibraryEmployeesController(LibraryManagementSystem.Server.Data.MyLibraryDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> GetLibraryEmployees()
        {
            var items = this.context.LibraryEmployees.AsQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>();
            this.OnLibraryEmployeesRead(ref items);

            return items;
        }

        partial void OnLibraryEmployeesRead(ref IQueryable<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> items);

        partial void OnLibraryEmployeeGet(ref SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/MyLibraryDB/LibraryEmployees(LibraryEmployeeID={LibraryEmployeeID})")]
        public SingleResult<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> GetLibraryEmployee(long key)
        {
            var items = this.context.LibraryEmployees.Where(i => i.LibraryEmployeeID == key);
            var result = SingleResult.Create(items);

            OnLibraryEmployeeGet(ref result);

            return result;
        }
        partial void OnLibraryEmployeeDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeDeleted(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        [HttpDelete("/odata/MyLibraryDB/LibraryEmployees(LibraryEmployeeID={LibraryEmployeeID})")]
        public IActionResult DeleteLibraryEmployee(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.LibraryEmployees
                    .Where(i => i.LibraryEmployeeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLibraryEmployeeDeleted(item);
                this.context.LibraryEmployees.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLibraryEmployeeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLibraryEmployeeUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeUpdated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        [HttpPut("/odata/MyLibraryDB/LibraryEmployees(LibraryEmployeeID={LibraryEmployeeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLibraryEmployee(long key, [FromBody]LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LibraryEmployees
                    .Where(i => i.LibraryEmployeeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLibraryEmployeeUpdated(item);
                this.context.LibraryEmployees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryEmployees.Where(i => i.LibraryEmployeeID == key);
                ;
                this.OnAfterLibraryEmployeeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/MyLibraryDB/LibraryEmployees(LibraryEmployeeID={LibraryEmployeeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLibraryEmployee(long key, [FromBody]Delta<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LibraryEmployees
                    .Where(i => i.LibraryEmployeeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLibraryEmployeeUpdated(item);
                this.context.LibraryEmployees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryEmployees.Where(i => i.LibraryEmployeeID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLibraryEmployeeCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);
        partial void OnAfterLibraryEmployeeCreated(LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee item)
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

                this.OnLibraryEmployeeCreated(item);
                this.context.LibraryEmployees.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LibraryEmployees.Where(i => i.LibraryEmployeeID == item.LibraryEmployeeID);

                ;

                this.OnAfterLibraryEmployeeCreated(item);

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

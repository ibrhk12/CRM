using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BusinessLayer.Department;
using CRM.BusinessLayer.InputModel;
using CRM.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;
        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }
        // GET: api/Department
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<Departments>>> getAllDepartments()
        {
            var result = await _departmentManager.GetAllDepartments();
            if (result != null)
            {
                return Ok(result);
            }
            else
                return BadRequest();
        }

        // GET: api/Department/5
        [HttpGet]
        public async Task<ActionResult<Departments>> Get([FromBody] DepartmentIM input)
        {
            var result = await _departmentManager.getDepartment(input.departmentName);
            if (result != null)
            {
                return Ok(result);
            }
            else
                return BadRequest();
        }

        // POST: api/Department
        //Create a new department
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepartmentIM input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _departmentManager.AddDepartment(input);
            return Ok();
        }

        // PUT: api/Department/5
        //update a Department
        [HttpPut]
        [Route("updateManager")]
        public async Task<IActionResult> updateManager([FromBody] DepartmentIM input)
        {
            var updated = await _departmentManager.setManager(input);
            if (updated)
            {
                return Ok(" Manager updated");
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions
        //Remove a department
        [HttpDelete]
        public void Delete([FromBody] DepartmentIM input)
        {
        }
    }
}

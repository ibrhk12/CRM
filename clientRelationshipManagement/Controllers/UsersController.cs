using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BusinessLayer.InputModel;
using CRM.DataAccess;
using CRM.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace clientRelationshipManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager _userManager;
        public UsersController(IUsersManager usersManager)
        {
            _userManager = usersManager;
        }
        // GET api/users/getAll
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<Users>>> getAll()
        {
            var result = await _userManager.GetAllUsers();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<Users>> Get([FromBody] UserCredentialsIM IM)
        {
            var result = await _userManager.GetUser(IM.userName);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        // POST api/users
        //Creates a new User
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] UserCredentialsIM IM)
        //{
        //    Users newUser = new Users
        //    {
        //        firstName = IM.firstName,
        //        lastName = IM.lastName,
        //        email = IM.email,
        //        userName = IM.userName,
        //        password = IM.password,
        //        hierarchy = IM.hierarchy,
        //        department = IM.department
        //    };
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var result = await _userManager.AddUser(newUser);
        //    if (!result.emailExist && !result.userNameExist)
        //    {
        //        return Ok(newUser);
        //    }
        //    return BadRequest(result.message);
        //}

        // PUT api/Users
        //updates User's data
        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] UserCredentialsIM IM)
        //{
        //    Users user = 
        //    var result = await _userManager.UpdateUser(IM.userName, user);
        //    if (result)
        //    {
        //        return Ok(new
        //        {
        //            user
        //        });
        //    }
        //    return BadRequest();
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string userName)
        //{
        //    var result = await _userManager.RemoveUser(userName);
        //    if(result)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
    }
}

using CodeForms.Context;
using CodeForms.Interface;
using CodeForms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForms.Controllers
{
    public class UserController : Controller
    {
 
        private IUserService _userService;
        public UserController ( IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("api/ListUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {

            return await _userService.GetAll();
        }

        [HttpPost("api/CreateUser")]
        public async Task<ActionResult<User>> CreateUser([FromBody]User user)
        {
            await _userService.CreateUser(user);

            return Created("GetAll", user);
        }

        [HttpGet("api/UserInfo")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
           
            return await _userService.GetUser(id);
        }

        [HttpPut("api/EditUser")]
        public async Task<ActionResult<User>> EditUser(int id, User user)
        {
            return await _userService.EditUser(id,user);
        }

        [HttpDelete("api/DeleteUser")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }



    }
}

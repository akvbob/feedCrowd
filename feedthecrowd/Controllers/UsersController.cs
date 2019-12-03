using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using FeedTheCrowd.Data;
using FeedTheCrowd.Services.Interfaces;
using FeedTheCrowd.Dtos.User;

namespace FeedTheCrowd.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller

    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UserDto user = new UserDto();
            if(id > 0)
            {
                user = await _userService.GetById(id);
            }
            var success = await _userService.Delete(id);
            if (success == false)
                return NotFound();

            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto newUser)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            await _userService.Update(id, newUser);
            return NoContent();
        }
    }
}
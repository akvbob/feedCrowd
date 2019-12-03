using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Auth;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FeedTheCrowd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IAuthService _authservice;

        public AuthController(IAuthService authService, IAuthRepository repo)
        {
            _authservice = authService;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegistrationDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                ModelState.AddModelError("username", "Username already exists");

            // validate the request UserForRegisterDto validations
            if (!ModelState.IsValid)
                return BadRequest("Username already exists");

            var user = await _authservice.Register(userForRegisterDto);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            var userFromRepo = await _authservice.Login(userForLoginDto);

            if (userFromRepo == null)
                return NotFound();

            return Ok(userFromRepo);
        }
    }
}
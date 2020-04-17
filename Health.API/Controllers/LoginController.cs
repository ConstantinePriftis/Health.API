using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Health.API.Dtos;
using Health.API.Models;
using Health.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Health.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private SignInManager<UserLoginDto> _manager;

        public IUsersRepository UsersRepository { get; }

        public LoginController(IConfiguration config, IUsersRepository usersRepository)
        {
            _config = config;
            UsersRepository = usersRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginDto login)
        {
            IActionResult response = Unauthorized();
            var user =  AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private  User AuthenticateUser(UserLoginDto login)
        {
            UserLoginDto user = null;
            var userFromDb = UsersRepository.GetUserByNameAndPassword(login.Name, login.Password);
           // var loginResult = await _manager.PasswordSignInAsync(login.Name, login.Password, false, false);
            //if(loginResult.)

            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            //if (login.Name == "Jignesh")
            //{
            //    user = new UserLoginDto { Name = "Jignesh Trivedi", Email = "test.btest@gmail.com" };
            //}
            return userFromDb;
        }
    }
}
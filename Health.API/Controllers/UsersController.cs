using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.API.Dtos;
using Health.API.Models;
using Health.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Health.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        private readonly ITemperatureRepository temperatureRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, ITemperatureRepository temperatureRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            this.temperatureRepository = temperatureRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserCreateDto user)
        {
            User newUser = new User(user.UserName, user.LastName, user.Password, user.Age);

            var modifiedRows = await _usersRepository.AddUserAsync(newUser);
            if (modifiedRows == 0)
                return NoContent();
            else
            {
                return Ok();
            }
        }



    }
}
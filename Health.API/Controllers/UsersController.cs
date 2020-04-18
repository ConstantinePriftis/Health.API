using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.API.Dtos;
using Health.API.Models;
using Health.API.ResourceParameters;
using Health.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
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

        [Authorize]
        [HttpGet()]
        [HttpHead]
        public IActionResult GetUsers([FromQuery] ResourceParams resourceParameters)
        {
            var usersCollection = _usersRepository.GetUsers(resourceParameters);

            if (usersCollection == null || usersCollection.Count() == 0)
                return NotFound("No users were found");
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersCollection));
        }


        [HttpGet("{userId}", Name = "GetUserById")]
        public IActionResult GetUserById(Guid userId)
        {
            var userInDb = _usersRepository.GetUserById(userId);
            if (userInDb == null)
                return NotFound();
            var dtoToReturn = _mapper.Map<UserDto>(userInDb);
            return Ok(dtoToReturn);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody]UserCreateDto user)
        {
            if (!TryValidateModel(user))
            {
                return ValidationProblem(ModelState);
            }
            var userToInsert = _mapper.Map<User>(user);
            _usersRepository.AddUser(userToInsert);
            _usersRepository.Save();
            return CreatedAtAction("GetUserById",
                 new { userId = userToInsert.Id }, _mapper.Map<UserDto>(userToInsert));
        }


        [HttpPut("{userId}")]
        public IActionResult ModifyUser(Guid userId, UserUpdateDto userUpdateDto)
        {
            var user = _usersRepository.GetUserById(userId);

            if (user == null)
            {
                if (string.IsNullOrWhiteSpace(userUpdateDto.Password)
                    || string.IsNullOrWhiteSpace(userUpdateDto.FirstName))
                    return BadRequest("In order to upsert a user , UserName and Password have to be provided");
                _usersRepository.AddUser(user);
                _usersRepository.Save();
                return CreatedAtAction("GetUserById",
                 new { userId = user.Id }, _mapper.Map<UserDto>(user));
            }
            else
            {
                user.ModifyUser(userUpdateDto.FirstName,
                    userUpdateDto.Password,
                    userUpdateDto.LastName,
                    userUpdateDto.Age);
            }
            _usersRepository.Save();
            return NoContent();
        }


        [HttpPatch("{userId}")]
        public IActionResult PartiallyUpdateUser(Guid userId,
           JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            var user = _usersRepository.GetUserById(userId);
            if (user == null)
                return NotFound();

            if (user == null)
            {
                var userDto = new UserUpdateDto();
                patchDocument.ApplyTo(userDto, ModelState);

                if (!TryValidateModel(userDto))
                {
                    return ValidationProblem(ModelState);
                }

                var userToAdd = _mapper.Map<User>(userDto);
                userToAdd.Id = userId;

                _usersRepository.AddUser(user);
                _usersRepository.Save();

                var dtoToReturn = _mapper.Map<UserDto>(userToAdd);

                return CreatedAtRoute("GetCourseForAuthor",
                    new { userId = user.Id },
                    dtoToReturn);

            }
            var userToPatch = _mapper.Map<UserUpdateDto>(user);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }


            _mapper.Map(userToPatch, user);

            if (string.IsNullOrWhiteSpace(userToPatch.Password)
                    || string.IsNullOrWhiteSpace(userToPatch.FirstName))
                return BadRequest("FirstName and password have to be provided");

            user.ModifyUser(userToPatch.FirstName, userToPatch.Password, userToPatch.LastName, userToPatch.Age);
            _usersRepository.Save();

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public IActionResult DeleteUser(Guid userId)
        {
            var user = _usersRepository.GetUserById(userId);
            if (user == null)
                return NotFound("The user you are trying to delete does not exist");
            _usersRepository.DeleteUser(user);
            _usersRepository.Save();
            return NoContent();
        }
    }
}

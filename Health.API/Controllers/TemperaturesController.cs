using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.API.Dtos;
using Health.API.Models;
using Health.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health.API.Controllers
{
    [Route("api/Users/{userId}/Temperatures")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        private readonly ITemperatureRepository temperatureRepository;
        private readonly IMapper _mapper;
        const int MinimumTemperature = 37;

        public TemperaturesController(IUsersRepository usersRepository,
            ITemperatureRepository temperatureRepository,
            IMapper mapper)
        {

            _usersRepository = usersRepository;
            this.temperatureRepository = temperatureRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetTemperatureById")]
        public async Task<IActionResult> GetTemperatureById(Guid userId, Guid temperatureId)
        {
            var entityFromDb = temperatureRepository.GetTemperatureById(temperatureId);
            var dtoToReturn = _mapper.Map<TemperatureDto>(entityFromDb);
            return Ok(dtoToReturn);
        }

        [HttpPost()]
        public IActionResult InsertTemperature(Guid userId, [FromQuery] double temperatureValue)
        {
            if (temperatureValue > MinimumTemperature)
            {
                //Create Fever
            }
            else
            {
                var temp = Temperature.Create(userId, temperatureValue);
                var dtoToReturn = _mapper.Map<TemperatureDto>(temp);

                temperatureRepository.AddTemperatureForUser(temp);
                return CreatedAtRoute("GetTemperatureById",
             new { userId = userId, temperatureId = temp.Id }, dtoToReturn);
            }
            return Ok();
        }

    }
}
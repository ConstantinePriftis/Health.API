using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.API.Dtos;
using Health.API.Models;
using Health.API.RepositoryServices;
using Health.API.ResourceParameters;
using Health.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health.API.Controllers
{
    [Route("api/Users/{userId}/Temperatures")]
    [ApiController]
    [Authorize]
    public class TemperaturesController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly IFeverRepository _feverRepository;
        private readonly IMapper _mapper;
        const int MinimumTemperature = 37;

        public TemperaturesController(IUsersRepository usersRepository,
            ITemperatureRepository temperatureRepository,
            IFeverRepository feverRepository,
            IMapper mapper)
        {

            _usersRepository = usersRepository;
            this._temperatureRepository = temperatureRepository;
            this._feverRepository = feverRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// If no more than 10 values are being returned
        /// pageSize has to be provided with a number > 10
        /// in the request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resourceParameters"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetTemperaturesForUser")]
        [HttpHead]
        public IActionResult GetTemperaturesForUser(Guid userId, [FromQuery] ResourceParameters.ResourceParams resourceParameters)
        {
            var collectionToReturn = _temperatureRepository.GetTemperaturesByUserId(userId, resourceParameters);
            if (collectionToReturn == null || collectionToReturn.Count() == 0)
                return NotFound("No temperatures were found for this user");
            return Ok(_mapper.Map<IEnumerable<TemperatureDto>>(collectionToReturn));
        }

        [HttpGet("{temperatureId}", Name = "GetTemperatureById")]
        public IActionResult GetTemperatureById(Guid userId, Guid temperatureId)
        {
            var entityFromDb = _temperatureRepository.GetTemperatureById(temperatureId, userId);
            if (entityFromDb == null)
                return NotFound();
            var dtoToReturn = _mapper.Map<TemperatureDto>(entityFromDb);
            return Ok(dtoToReturn);
        }

        [HttpGet("Fevers")]
        public IActionResult GetFeversForUser(Guid userId)
        {
            var fevers = _feverRepository.GetFeversForUSer(userId);
            if (fevers == null)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<FeverDto>>(fevers));
        }

        [HttpPost()]
        public IActionResult InsertTemperature(Guid userId, [FromQuery] double temperature)
        {
            bool hasFever = false;
            var user = _usersRepository.GetUserById(userId);
            if (user == null)
                return NotFound("User provided was not found");
            string errorMessage = "";
            if (!ValidateTemperature(temperature, ref errorMessage))
                return BadRequest(errorMessage);
            Temperature temp = null;
            if (temperature > MinimumTemperature)
            {
                hasFever = true;
                temp = Temperature.Create(userId, temperature, hasFever);

                var dtoToReturn = _mapper.Map<TemperatureDto>(temp);
                _temperatureRepository.AddTemperatureForUser(temp);
                _feverRepository.AddFever(Fever.Create(userId, temperature));
                _temperatureRepository.Save();
                return CreatedAtRoute("GetTemperatureById",
             new { userId = userId, temperatureId = temp.Id }, dtoToReturn);
            }
            else
            {
                hasFever = false;
                temp = Temperature.Create(userId, temperature, hasFever);
                var dtoToReturn = _mapper.Map<TemperatureDto>(temp);

                _temperatureRepository.AddTemperatureForUser(temp);
                _temperatureRepository.Save();
                return CreatedAtRoute("GetTemperatureById",
             new { userId = userId, temperatureId = temp.Id }, dtoToReturn);
            }
        }

        [HttpPut("{temperatureId}")]
        public IActionResult UpdateTemperature(Guid userId, Guid temperatureId, [FromQuery] double temperature)
        {
            var user = _usersRepository.GetUserById(userId);
            if (user == null)
                return NotFound("User provided was not found");
            var validationErrorMessage = string.Empty;
            if (!ValidateTemperature(temperature, ref validationErrorMessage))
                return BadRequest(validationErrorMessage);

            var entityFromDb = _temperatureRepository.GetTemperatureById(temperatureId, userId);
            if (entityFromDb == null)
            {
                if (temperature > MinimumTemperature)
                    _feverRepository.AddFever(Fever.Create(userId, temperature));
                var temp = temperature > MinimumTemperature
                    ? Temperature.Create(userId, temperature, true)
                : Temperature.Create(userId, temperature);
                _temperatureRepository.AddTemperatureForUser(temp);
                var dtoToReturn = _mapper.Map<TemperatureDto>(temp);
                return CreatedAtRoute("GetTemperatureById",
             new { userId = userId, temperatureId = temp.Id }, dtoToReturn);
            }
            else
            {

                if (temperature > MinimumTemperature)
                {
                    entityFromDb.ModifyTemperature(temperature, true);
                    _feverRepository.AddFever(Fever.Create(userId, temperature));
                }

                entityFromDb.ModifyTemperature(temperature, false);
                _temperatureRepository.Save();
            }
            return NoContent();
        }

        /// <summary>
        /// The patch command was not added since the temperature is
        /// consisted of only the temperature value.
        /// The same effect happes in put Action
        /// </summary>

        [HttpDelete("{temperatureId}")]
        public IActionResult RemoveTemperature(Guid userId, Guid temperatureId)
        {
            var user = _usersRepository.GetUserById(userId);
            if (user == null)
                return NotFound("User provided was not found");

            var persistedTemperature = _temperatureRepository.GetTemperatureById(temperatureId, userId);

            if (persistedTemperature == null)
                return NotFound("The provided temperature does not exist");

            _temperatureRepository.DeleteTemperature(persistedTemperature);
            _temperatureRepository.Save();
            return NoContent();
        }

        private bool ValidateTemperature(double temperature, ref string errorMessage)
        {
            bool isValid = false;
            if (temperature > 42)
            {
                errorMessage = "The provided temperature value must be bellow 42 degrees";
                return isValid;
            }

            if (temperature < 35)
            {
                errorMessage = "The provided temperature value must be above 35 degrees";
                return isValid;
            }
            isValid = true;
            return isValid;
        }

    }
}
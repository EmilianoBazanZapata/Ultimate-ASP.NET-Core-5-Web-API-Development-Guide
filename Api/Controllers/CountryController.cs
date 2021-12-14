using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.IRepository;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var Countries = await _unitofwork.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDTO>>(Countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetCountries)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet("{id:int}",Name = "GetCountryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var Country = await _unitofwork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somehing Went Wrong in the {nameof(GetCountryById)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = _mapper.Map<Country>(countryDTO);
                await _unitofwork.Countries.Insert(country);
                await _unitofwork.Save();

                return CreatedAtRoute("GetCountryById", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Went Wrong in the {nameof(AddCountry)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
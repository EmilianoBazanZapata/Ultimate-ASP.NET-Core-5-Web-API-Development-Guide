using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.IRepository;
using Api.Models;
using AutoMapper;
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.IRepository;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitofwork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <IActionResult> GetAllHotels()
        {
            try
            {
                 var Hotels = await _unitofwork.Hotels.GetAll();
                 var results = _mapper.Map<IList<HotelDTO>>(Hotels);
                 return  Ok(results);
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Something Went Wrong in the {nameof(GetAllHotels)}",ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <IActionResult> GetHotelById(int id)
        {
            try
            {
                 var Hotel = await _unitofwork.Hotels.Get(p => p.Id == id, new List<string> { "Country" });
                 var result = _mapper.Map<HotelDTO>(Hotel);
                 return  Ok(result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Something Went Wrong in the {nameof(GetHotelById)}",ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
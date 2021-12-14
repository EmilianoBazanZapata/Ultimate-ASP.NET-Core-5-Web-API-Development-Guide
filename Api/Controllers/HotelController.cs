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
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var Hotels = await _unitofwork.Hotels.GetAll();
                var results = _mapper.Map<IList<HotelDTO>>(Hotels);
                return Ok(results);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Went Wrong in the {nameof(GetAllHotels)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotelById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotelById(int id)
        {
            try
            {
                var Hotel = await _unitofwork.Hotels.Get(p => p.Id == id, new List<string> { "Country" });
                var result = _mapper.Map<HotelDTO>(Hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Went Wrong in the {nameof(GetHotelById)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in{nameof(AddHotel)}");
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitofwork.Hotels.Insert(hotel);
                await _unitofwork.Save();

                return CreatedAtRoute("GetHotelById", new { id = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Went Wrong in the {nameof(AddHotel)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = await _unitofwork.Hotels.Get(q => q.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHotel)}");
                    return BadRequest("Submitted data is Invalid");
                }
                _mapper.Map(hotelDTO, hotel);
                _unitofwork.Hotels.Update(hotel);
                await _unitofwork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Went Wrong in the {nameof(UpdateHotel)}", ex);
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
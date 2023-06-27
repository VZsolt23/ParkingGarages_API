using Microsoft.AspNetCore.Mvc;
using ParkingGarages_API.Models.DTO;
using ParkingGarages_API.Repositories;
using ParkingGarages_API.Repositories.Impl;

namespace ParkingGarages_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingAPIController : ControllerBase
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingAPIController(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingDTO>>> GetAllParkings()
        {
            var parkings = await _parkingRepository.GetOnGoingParkingsAsync();
            return Ok(parkings);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ParkingDTO>> CreateGarage([FromBody] ParkingDTO parkingDTO)
        {
            if (parkingDTO == null)
            {
                return BadRequest(parkingDTO);
            }

            if (parkingDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ParkingDTO parking;

            try
            {
                parking = await _parkingRepository.CreateParkingAsync(parkingDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtRoute("GetGarage", new { id = parking.Id }, parking);
        }
    }
}

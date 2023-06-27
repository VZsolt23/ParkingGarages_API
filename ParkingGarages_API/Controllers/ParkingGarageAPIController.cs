using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Data;
using ParkingGarages_API.Exceptions;
using ParkingGarages_API.Models.DTO;
using ParkingGarages_API.Repositories;

namespace ParkingGarages_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingGarageAPIController : ControllerBase
    {

        private readonly IParkingGarageRepository _parkingGarageRepository;

        public ParkingGarageAPIController(IParkingGarageRepository parkingGarageRepository)
        {
            _parkingGarageRepository = parkingGarageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingGarageDTO>>> GetAllGarages()
        {
            var garages = await _parkingGarageRepository.GetAllGaragesAsync();
            return Ok(garages);
        }

        [HttpGet("{id:int}", Name = "GetGarage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParkingGarageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParkingGarageDTO>> GetGarage(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var garage = await _parkingGarageRepository.GetGarageAsync(id);

            if (garage == null)
            {
                return NotFound();
            }

            return Ok(garage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ParkingGarageDTO>> CreateGarage([FromBody] ParkingGarageDTO parkingGarageDTO)
        {
            if (parkingGarageDTO == null)
            {
                return BadRequest(parkingGarageDTO);
            }

            if (parkingGarageDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var garageDTO = await _parkingGarageRepository.CreateGarageAsync(parkingGarageDTO);

            return CreatedAtRoute("GetGarage", new { id = garageDTO.Id }, garageDTO);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGarage(int id, [FromBody] ParkingGarageDTO parkingGarageDTO)
        {
            if (parkingGarageDTO == null || id != parkingGarageDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _parkingGarageRepository.UpdateGarageAsync(id, parkingGarageDTO);
            }
            catch (GarageNotFound e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialGarage(int id, JsonPatchDocument<ParkingGarageDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            try
            {
                await _parkingGarageRepository.UpdatePartialGarageAsync(id, patchDTO);
            }
            catch (GarageNotFound e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGarage(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _parkingGarageRepository.DeleteGarageAsync(id);
            }
            catch (GarageNotFound e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}

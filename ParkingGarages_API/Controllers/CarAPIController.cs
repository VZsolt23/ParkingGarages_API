using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ParkingGarages_API.Exceptions;
using ParkingGarages_API.Models.DTO;
using ParkingGarages_API.Repositories;

namespace ParkingGarages_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarAPIController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarAPIController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCarssAsync();
            return Ok(cars);
        }

        [HttpGet("{id:int}", Name = "GetCar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var car = await _carRepository.GetCarAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarDTO>> CreateCar([FromBody] CarDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest(carDTO);
            }

            if (carDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
         
            try
            {
                await _carRepository.CreateCarAsync(carDTO);
            }
            catch (CarConflict e)
            {
                return Conflict(new { error = e.Message });
            }

            return CreatedAtRoute("GetCar", new { id = carDTO.Id } ,carDTO);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarDTO carDTO)
        {
            if (carDTO == null || id != carDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _carRepository.UpdateCarAsync(id, carDTO);
            }
            catch (CarNotFound e)
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
        public async Task<IActionResult> UpdatePartialCar(int id, JsonPatchDocument<CarDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            try
            {
                await _carRepository.UpdatePartialCarAsync(id, patchDTO);
            }
            catch (CarNotFound e)
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
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _carRepository.DeleteCarAsync(id);
            }
            catch (CarNotFound e)
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

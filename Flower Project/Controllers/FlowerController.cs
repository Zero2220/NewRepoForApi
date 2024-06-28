using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Dtos.FlowerDtos;
using Service.Interfaces;

namespace Flower_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] FlowerCreateDto flowerCreateDto)
        {
            if (flowerCreateDto == null)
            {
                return BadRequest("Invalid flower data.");
            }

            try
            {
                var flowerId = _flowerService.Create(flowerCreateDto);
                return Ok(new { id = flowerId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid flower ID.");
            }

            try
            {
                _flowerService.Delete(id);
                return Ok("Flower deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromForm] FlowerEditDto flowerEditDto)
        {
            if (flowerEditDto == null)
            {
                return BadRequest("Invalid flower data.");
            }

            try
            {
                _flowerService.Update(id, flowerEditDto);
                return Ok("Flower updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<GetFlowerDto> GetById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid flower ID.");
            }

            try
            {
                var flowerDto = _flowerService.GetById(id);
                if (flowerDto == null)
                {
                    return NotFound("Flower not found.");
                }

                return Ok(flowerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<PaginatedList<GetFlowerDto>> GetAll(int size, int page)
        {
            try
            {
                var paginatedList = _flowerService.GetAllPaginated(size, page);
                return Ok(paginatedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

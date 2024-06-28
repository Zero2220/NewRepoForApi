using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Dtos.SliderDtos;
using Service.Interfaces;

namespace Slider_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] SliderCreateDto sliderCreateDto)
        {
            if (sliderCreateDto == null)
            {
                return BadRequest("Invalid slider data.");
            }

            try
            {
                _sliderService.Create(sliderCreateDto);
                return Ok("Slider created successfully.");
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
                return BadRequest("Invalid slider ID.");
            }

            try
            {
                _sliderService.Delete(id);
                return Ok("Slider deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromForm] SliderCreateDto sliderCreateDto)
        {
            if (sliderCreateDto == null)
            {
                return BadRequest("Invalid slider data.");
            }

            try
            {
                _sliderService.Update(id, sliderCreateDto);
                return Ok("Slider updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<SliderGetDto> GetById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid slider ID.");
            }

            try
            {
                var sliderDto = _sliderService.GetById(id);
                if (sliderDto == null)
                {
                    return NotFound("Slider not found.");
                }

                return Ok(sliderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<PaginatedList<SliderGetDto>> GetAll(int size, int page)
        {
            try
            {
                var paginatedList = _sliderService.GetAllPaginated(size, page);
                return Ok(paginatedList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

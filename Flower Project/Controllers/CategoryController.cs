using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace Flower_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService =  categoryService;
        }


        [HttpPost("")]
        public IActionResult Create(CategoryCreateDto createDto)
        {
            if(createDto == null)
            {
                return BadRequest();
            }
             _categoryService.Create(createDto);

            return Ok();
        }

        [HttpPut("")]
        public IActionResult Edit(CategoryEditDto editDto)
        {
            if(editDto == null)
            {
                return BadRequest();
            }
            _categoryService.Update(editDto);

            return Ok();
        }

        [HttpDelete("")]
        public IActionResult Remove(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            _categoryService.Remove(id);

            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<GetCategoryDto>> GetAll()
        {
            List<GetCategoryDto> categories = _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("Id")]
        public ActionResult<GetCategoryDto> GetById(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }

             return _categoryService.GetById(id);
        }

        
    }
}

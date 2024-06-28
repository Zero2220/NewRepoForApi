using Microsoft.AspNetCore.Mvc;
using Ui.Models.Sliders;
using Ui.Services.Interfaces;

namespace Ui.Controllers
{
    public class SliderController : Controller
    {
        private readonly ICrudService _crudService;
        public SliderController(ICrudService crudService)
        {
            _crudService = crudService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateRequest sliderCreateRequest)
        {
            if (sliderCreateRequest == null)
            {
                return BadRequest();
            }
            await _crudService.CreateFromForm(sliderCreateRequest,"Slider/Create");
            
            return View();
        }
    }
}

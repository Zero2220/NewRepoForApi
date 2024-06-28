using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos.FlowerDtos
{
    public class FlowerCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price {  get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> FormFiles { get; set; }
        

    }


    public class FlowerCreateDtoValidator : AbstractValidator<FlowerCreateDto>
    {
        public FlowerCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100).MinimumLength(10);
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.FormFiles).NotEmpty();

        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Http;
using Service.Dtos.FlowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos.SliderDtos
{
    public class SliderCreateDto
    {
        public string Word1 { get; set; }
        public string RedWord { get; set; }
        public string Word2 { get; set; }
        public string Text2 { get; set; }
        public IFormFile formFile { get; set; }
    }

    public class SliderCreateDtoValidator : AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValidator()
        {
            RuleFor(x => x.Word1).NotEmpty().MaximumLength(30).MinimumLength(3);
            RuleFor(x => x.Word2).NotEmpty().MaximumLength(30).MinimumLength(3);
            RuleFor(x => x.RedWord).NotNull().MaximumLength(30).MinimumLength(3);
            RuleFor(x => x.Text2).NotNull().MaximumLength(70).MinimumLength(3);
            RuleFor(x => x.formFile).NotEmpty();

        }
    }
}

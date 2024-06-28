using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Service.Dtos;
using Service.Dtos.FlowerDtos;
using Service.Dtos.SliderDtos;

namespace Service.Profiles
{
    public class MappingProfile : Profile
    {
        private readonly IHttpContextAccessor _context;

        public MappingProfile(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;

            var uriBuilder = new UriBuilder(
                _context.HttpContext.Request.Scheme,
                _context.HttpContext.Request.Host.Host,
                _context.HttpContext.Request.Host.Port ?? -1  
            );
            string baseUrl = uriBuilder.Uri.AbsoluteUri;

            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();
            CreateMap<FlowerCreateDto, Flower>().ReverseMap();
            CreateMap<GetFlowerDto, Flower>();
            CreateMap<SliderCreateDto, Slider>().ReverseMap();
            CreateMap<SliderGetDto, Slider>().ReverseMap();

            CreateMap<Flower, GetFlowerDto>()
               .ForMember(dest => dest.ImageUrl,
                          opt => opt.MapFrom(src =>
                              src.FlowerImages.Where(img => img.Status).Select(img => $"{baseUrl}/uploads/flowers/{img.ImageName}").ToList()));
        }
    }
}

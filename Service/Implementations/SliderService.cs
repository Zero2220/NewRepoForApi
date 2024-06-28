using AutoMapper;
using Core.Entities;
using Data.Repostitories.Interfaces;
using Service.Dtos;
using Service.Dtos.SliderDtos;
using Service.Interfaces;
using Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper; 

        public SliderService(ISliderRepository sliderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sliderRepository = sliderRepository;
        }

        public void Create(SliderCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Slider slider = _mapper.Map<SliderCreateDto, Slider>(dto);

            slider.ImageName = dto.formFile.Save("uploads/sliders");

            _sliderRepository.Add(slider);
            _sliderRepository.Save();
        }

        public void Delete(int id)
        {
            if (!_sliderRepository.Exists(x => x.Id == id))
            {
                throw new Exception("Slider not found");
            }

            Slider slider = _sliderRepository.Get(x => x.Id == id);
            slider.IsDeleted = true;

            _sliderRepository.Save();
        }

        public PaginatedList<SliderGetDto> GetAllPaginated(int size, int page)
        {
            var query = _sliderRepository.GetAll(x => !x.IsDeleted);
            PaginatedList<Slider> sliders = PaginatedList<Slider>.Create(query, page, size);

            var sliderDtos = sliders.Items.Select(slider =>
            {
                var sliderDto = _mapper.Map<SliderGetDto>(slider);
                sliderDto.ImageUrl = $"uploads/sliders/{slider.ImageName}";
                return sliderDto;
            }).ToList();

            return new PaginatedList<SliderGetDto>(sliderDtos, sliders.TotalPages, sliders.PageIndex, sliders.PageSize);
        }

        public SliderGetDto GetById(int id)
        {
            Slider slider = _sliderRepository.Get(x => x.Id == id && !x.IsDeleted);

            if (slider == null)
            {
                throw new Exception("Slider not found");
            }

            var sliderDto = _mapper.Map<SliderGetDto>(slider);
            sliderDto.ImageUrl = $"uploads/sliders/{slider.ImageName}";

            return sliderDto;
        }

        public void Update(int id, SliderCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Slider slider = _sliderRepository.Get(x => x.Id == id && !x.IsDeleted);

            if (slider == null)
            {
                throw new Exception("Slider not found");
            }

            slider.Word1 = dto.Word1;
            slider.RedWord = dto.RedWord;
            slider.Word2 = dto.Word2;
            slider.Text2 = dto.Text2;

            if (dto.formFile != null)
            {
                slider.ImageName = dto.formFile.Save("uploads/sliders");
            }

            _sliderRepository.Save();
        }
    }
}

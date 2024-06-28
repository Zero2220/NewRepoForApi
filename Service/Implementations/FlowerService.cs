using AutoMapper;
using Core.Entities;
using Data.Repostitories.Interfaces;
using Service.Dtos;
using Service.Dtos.FlowerDtos;
using Service.Extensions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementations
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public FlowerService(IFlowerRepository repository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _flowerRepository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public int Create(FlowerCreateDto flowerCreateDto)
        {
            if (flowerCreateDto == null)
                throw new ArgumentNullException();

            if (_flowerRepository.Exists(x => x.Name == flowerCreateDto.Name))
                throw new ArgumentException();

            List<string> fileNames = new List<string>();

            foreach (var item in flowerCreateDto.FormFiles)
            {
                var imageName = item.Save("uploads/flowers");
                fileNames.Add(imageName);
            }

            Flower flower = _mapper.Map<FlowerCreateDto, Flower>(flowerCreateDto);

            flower.CategoryId = flowerCreateDto.CategoryId;

            flower.FlowerImages = fileNames.Select(fileName => new FlowerImage
            {
                ImageName = fileName,
                Flower = flower,
                Status = true 
            }).ToList();

            _flowerRepository.Add(flower);
            _flowerRepository.Save();

            return flower.Id;
        }

        public void Delete(int id)
        {
            Flower flower = _flowerRepository.Get(x => !x.IsDeleted && x.Id == id);

            if (flower == null)
                throw new Exception("Flower not found");

            flower.IsDeleted = true;
            flower.ModifiedAt = DateTime.Now;

            _flowerRepository.Save();
        }

        public void Update(int id, FlowerEditDto flowerEditDto)
        {
            Flower flower = _flowerRepository.Get(x => x.Id == id && !x.IsDeleted, "FlowerImages");

            if (flower == null)
            {
                throw new Exception("Flower not found");
            }

            if (!_categoryRepository.Exists(x => x.Id == flowerEditDto.CategoryId))
            {
                throw new Exception("Category not found");
            }

            var imagesToRemove = flower.FlowerImages.Where(img => !flowerEditDto.FileIds.Contains(img.Id) && img.Status).ToList();
            foreach (var img in imagesToRemove)
            {
                img.Status = false;
            }

            List<string> fileNames = new List<string>();
            foreach (var item in flowerEditDto.FormFiles)
            {
                var imageName = item.Save("uploads/flowers");
                fileNames.Add(imageName);
            }

            flower.Price = flowerEditDto.Price;
            flower.ModifiedAt = DateTime.Now;
            flower.CategoryId = flowerEditDto.CategoryId;
            flower.Description = flowerEditDto.Description;
            flower.Name = flowerEditDto.Name;

            var newImages = fileNames.Select(fileName => new FlowerImage
            {
                ImageName = fileName,
                Flower = flower,
                FlowerId = flower.Id,
                Status = true 
            }).ToList();

            flower.FlowerImages.AddRange(newImages);

            _flowerRepository.Save();
        }

        public GetFlowerDto GetById(int id)
        {
            Flower flower = _flowerRepository.Get(x => x.Id == id && !x.IsDeleted, "FlowerImages");

            if (flower == null)
                throw new Exception("Flower not found");

            var getFlowerDto = _mapper.Map<GetFlowerDto>(flower);
            getFlowerDto.ImageUrl = flower.FlowerImages
                .Where(img => img.Status)
                .Select(img => img.ImageName)
                .ToList();

            return getFlowerDto;
        }

        public PaginatedList<GetFlowerDto> GetAllPaginated(int size, int page)
        {
            var query = _flowerRepository.GetAll(x => !x.IsDeleted);

            PaginatedList<Flower> flowers = PaginatedList<Flower>.Create(query, page, size);

            var getFlowerDtos = flowers.Items.Select(flower =>
            {
                var getFlowerDto = _mapper.Map<GetFlowerDto>(flower);
                getFlowerDto.ImageUrl = flower.FlowerImages
                    .Where(img => img.Status)
                    .Select(img => img.ImageName)
                    .ToList();
                return getFlowerDto;
            }).ToList();

            return new PaginatedList<GetFlowerDto>(getFlowerDtos, flowers.TotalPages, flowers.PageIndex, flowers.PageSize);
        }
    }
}

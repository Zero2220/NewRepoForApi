using AutoMapper;
using Core.Entities;
using Data;
using Data.Repostitories.Implementations;
using Data.Repostitories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Dtos;
using Service.Interfaces;
using Service.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CategoryService:ICategoryService
    {
       
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository category,IMapper autoMapper)
        {
            _mapper = autoMapper;
            _categoryRepository = category;
        }

        public int Create(CategoryCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException();

            if (_categoryRepository.Exists(x=>x.IsDeleted! && x.Name ==createDto.Name))
            {
                throw new ArgumentException();
            }
            Category category = _mapper.Map<CategoryCreateDto, Category>(createDto);

            _categoryRepository.Add(category);
            _categoryRepository.Save();
            return category.Id;
        }
        public void Update(CategoryEditDto editDto)
        {
            if(editDto == null) throw new ArgumentNullException();

            if(_categoryRepository.Exists(x=>x.IsDeleted! && x.Name ==editDto.Name)) { throw new ArgumentException(); }

            Category category = _categoryRepository.Get(x =>x.Id ==editDto.Id && !x.IsDeleted);

            category.ModifiedAt = DateTime.Now;

            category.Name = editDto.Name;

            _categoryRepository.Save();

        }

        public void Remove(int id)
        {
            if(id ==null) throw new ArgumentNullException();

            Category category = _categoryRepository.Get(x => x.Id == id);

            category.ModifiedAt = DateTime.UtcNow;

            category.IsDeleted = true;

            _categoryRepository.Save();
        }

        public List<GetCategoryDto> GetAll()
        {
            return _mapper.Map<List<GetCategoryDto>>(_categoryRepository.GetAll(x => !x.IsDeleted).ToList());
        }

        public GetCategoryDto GetById(int id)
        {
            Category category = _categoryRepository.Get(x =>x.Id ==id && !x.IsDeleted);

            GetCategoryDto dto = _mapper.Map<Category, GetCategoryDto>(category);

            return dto;
        }
    }
}

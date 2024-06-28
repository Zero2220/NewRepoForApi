using Core.Entities;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        public int Create(CategoryCreateDto categoryCreateDto);
        void Update(CategoryEditDto editDto);
        void Remove(int id);
        List<GetCategoryDto> GetAll();
        GetCategoryDto GetById(int id);
    }
}

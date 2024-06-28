using Core.Entities;
using Data.Repostitories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repostitories.Implementations
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FlowerStoreDbContext context) : base(context)
        {

        }
    }
}

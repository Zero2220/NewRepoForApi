using Service.Dtos;
using Service.Dtos.FlowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFlowerService
    {

        public int Create(FlowerCreateDto flowerCreateDto);
        public void Update(int id,FlowerEditDto flowerEditDto);
        public void Delete(int id);
        public PaginatedList<GetFlowerDto> GetAllPaginated(int size,int page);
        public GetFlowerDto GetById(int id);

    }
}

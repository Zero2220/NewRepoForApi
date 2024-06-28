using Service.Dtos.FlowerDtos;
using Service.Dtos;
using Service.Dtos.SliderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISliderService
    {

        public void Create(SliderCreateDto dto);
        public void Update(int id,SliderCreateDto dto);
        public void Delete(int id);
        public SliderGetDto GetById(int id);
        public PaginatedList<SliderGetDto> GetAllPaginated(int size, int page);

    }
}

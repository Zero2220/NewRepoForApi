using Core.Entities;
using Data.Repostitories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Repostitories.Implementations
{
    public class SliderRepository: Repository<Slider>, ISliderRepository
    {
        public SliderRepository(FlowerStoreDbContext context) : base(context)
        {

        }
    }
}

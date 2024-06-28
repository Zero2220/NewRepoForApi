using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos.SliderDtos
{
    public class SliderGetDto
    {
        public string Word1 { get; set; }
        public string RedWord { get; set; }
        public string Word2 { get; set; }
        public string Text2 { get; set; }
        public string ImageUrl {  get; set; }
    }
}

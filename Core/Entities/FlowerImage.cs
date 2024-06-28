using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FlowerImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int FlowerId { get; set; }
        public Flower Flower { get; set; }
        public bool Status { get; set; }
    }
}

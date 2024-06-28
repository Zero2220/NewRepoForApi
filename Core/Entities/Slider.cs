using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Slider:AuditEntity
    {
        public int Id { get; set; }
        public string Word1 { get; set; }
        public string RedWord {  get; set; }
        public string Word2 { get; set; }
        public string Text2 { get; set; }
        public string ImageName { get; set; }
        
    }
}

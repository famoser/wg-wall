using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Dto.Base;

namespace WgWall.Dto
{
    public class ProductDto : BaseIdEntityDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int? BoughtById { get; set; }
        public bool Hide { get; set; }
    }
}

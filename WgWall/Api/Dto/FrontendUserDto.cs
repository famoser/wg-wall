using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Dto.Base;

namespace WgWall.Dto
{
    public class FrontendUserDto : BaseIdEntityDto
    {
        public string Name { get; set; }
        public int Karma { get; set; }
        public string ProfileImageSrc { get; set; }
    }
}

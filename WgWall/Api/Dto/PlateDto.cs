using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Dto.Base;

namespace WgWall.Api.Dto
{
    public class PlateDto : BaseDto
    {
        public int FrontendUserId { get; set; }
        public int DinnerState { get; set; }
    }
}

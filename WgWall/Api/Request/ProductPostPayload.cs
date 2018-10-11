using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class ProductPostPayload : AccountablePayload
    {
        public string Name { get; set; }
    }
}

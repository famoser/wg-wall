using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Base;

namespace WgWall.Api.Request
{
    public class ProductPutPayload : AccountablePayload
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int BoughtBy { get; set; }
    }
}

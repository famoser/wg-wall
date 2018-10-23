using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Interface;
using WgWall.Data.Model;

namespace WgWall.Api.Request
{
    public class FrontendUserPayload : IWriteIntoPayload<FrontendUser>
    {
        public string Name { get; set; }
        public void WriteInto(FrontendUser entity)
        {
            entity.Name = Name;
        }
    }
}

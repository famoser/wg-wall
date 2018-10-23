using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WgWall.Api.Request
{
    public class CalenderEntryPayload
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}

using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}

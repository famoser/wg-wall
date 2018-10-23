using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class CalenderEntry : AccountableEntity
    {
        public string Title { get; set; }
        public DateTime OccursAt { get; set; }
    }
}

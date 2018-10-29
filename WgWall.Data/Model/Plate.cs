using System;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class Plate : AccountableEntity
    {
        public DateTime ValidityDate { get; set; }
        public int DinnerState { get; set; }
    }
}

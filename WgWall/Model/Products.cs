using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Model.Base;

namespace WgWall.Model
{
    public class Products : BaseEntity
    {
        public string Text { get; set; }
        public int Amount { get; set; }
        public int BoughtById { get; set; }
        public virtual FrontendUser BoughtBy { get; set; }
    }
}

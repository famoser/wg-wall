using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Model.Base;

namespace WgWall.Model
{
    public class FrontendUser : BaseEntity
    {
        public string Name { get; set; }

        [InverseProperty("BoughtBy")]
        public virtual List<Products> BoughtProducts { get; set; }
    }
}

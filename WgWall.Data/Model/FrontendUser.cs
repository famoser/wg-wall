using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WgWall.Data.Model.Base;

namespace WgWall.Data.Model
{
    public class FrontendUser : BaseEntity
    {
        public string Name { get; set; }
        public int Karma { get; set; }
        public string ProfileImageSrc { get; set; }

        [InverseProperty("Accountable")]
        public virtual List<TaskExecution> ExecutedTasks { get; set; }

        [InverseProperty("Accountable")]
        public virtual List<ProductPurchase> PurchasedProducts { get; set; }
    }
}

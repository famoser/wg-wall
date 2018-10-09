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

        [InverseProperty("BoughtBy")]
        public virtual List<Product> BoughtProducts { get; set; }

        public static FrontendUser Create(string name, FrontendUser createdBy =null)
        {
            var element = new FrontendUser {Name = name};
            RegisterCreation(element, createdBy);
            return element;
        }
    }
}

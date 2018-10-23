using System;

namespace WgWall.Data.Model.Base
{
    public abstract class AccountableEntity : BaseEntity
    {
        public int? AccountableId { get; set; }
        public virtual FrontendUser Accountable { get; set; }
    }
}

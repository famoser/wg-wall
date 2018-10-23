namespace WgWall.Data.Model.Base
{
    public abstract class AccountableEntity : BaseEntity
    {
        public virtual FrontendUser Accountable { get; set; }
    }
}

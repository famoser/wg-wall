using System;

namespace WgWall.Data.Model.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public virtual FrontendUser CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        protected static T RegisterCreation<T>(T self, FrontendUser frontendUser)
        where T : BaseEntity
        {
            self.CreatedAt = new DateTime();
            self.CreatedBy = frontendUser;
            return self;
        }

    }
}

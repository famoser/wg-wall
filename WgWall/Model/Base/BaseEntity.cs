using System;

namespace WgWall.Model.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public virtual FrontendUser CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

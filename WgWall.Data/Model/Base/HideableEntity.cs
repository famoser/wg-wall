using System;

namespace WgWall.Data.Model.Base
{
    public abstract class HideableEntity : BaseEntity
    {
        public bool IsHidden { get; set; }
    }
}

using System;

namespace WgWall.Data.Model.Base
{
    public abstract class TrackActionEntity<T> : BaseEntity
    {
        public DateTime ExecutedAt { get; set; }

        public int EntityId { get; set; }
        public virtual T Entity { get; set; }

        public int? DoneById { get; set; }
        public virtual FrontendUser DoneBy { get; set; }
    }
}

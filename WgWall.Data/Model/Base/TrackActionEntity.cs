using System;

namespace WgWall.Data.Model.Base
{
    public abstract class TrackActionEntity<T> : AccountableEntity
    {
        public DateTime ExecutedAt { get; set; }
        public virtual T Entity { get; set; }
        public int KarmaEarned { get; set; }
    }
}

using System.Collections.Generic;
using WgWall.Data.Model.Base;

namespace WgWall.Test.Utils.SampleData.Interface
{
    public interface ISampleDataService
    {
        List<BaseEntity> LoadEntities();
    }
}

using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base.Interfaces;

namespace WgWall.Data.Repository.Interfaces
{
    public interface IFrontendUserRepository : IGetRepository<FrontendUser>
    {
        Task RecalculateKarma<T, T2>(FrontendUser frontendUser, T newKarmaSource) where T : TrackActionEntity<T2>;
    }
}

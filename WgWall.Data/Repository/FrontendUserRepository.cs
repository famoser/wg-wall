using System.Linq;
using System.Threading.Tasks;
using WgWall.Data.Model;
using WgWall.Data.Model.Base;
using WgWall.Data.Repository.Base;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Data.Repository
{
    public class FrontendUserRepository : CrudRepository<FrontendUser>, IFrontendUserRepository
    {
        public FrontendUserRepository(MyDbContext context) : base(context)
        {
        }

        private async Task RecalculateKarma(FrontendUser frontendUser, int offset)
        {
            frontendUser.Karma = offset;

            frontendUser.Karma += frontendUser.ExecutedTasks.Sum(t => t.KarmaEarned);
            frontendUser.Karma += frontendUser.PurchasedProducts.Sum(t => t.KarmaEarned);

            await Context.SaveChangesAsync();
        }

        public Task RecalculateKarma<T, T2>(FrontendUser frontendUser, T newKarmaSource) where T : TrackActionEntity<T2>
        {
            return RecalculateKarma(frontendUser, Context.Set<T>().Local.Any(e => e == newKarmaSource) ? 0 : newKarmaSource.KarmaEarned);
        }
    }
}

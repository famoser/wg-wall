using System.Threading.Tasks;

namespace WgWall.Test.Utils.IntegrationTest.Interface
{
    public interface ITestClient
    {
        Task<object> GetJsonAsync(string link);

        Task<object> PostJsonAsync(string link, object content);
    }
}

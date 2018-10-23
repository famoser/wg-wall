using System.Threading.Tasks;

namespace WgWall.Test.Utils.Interface
{
    public interface ITestClient
    {
        Task<object> GetJsonAsync(string link);
        Task<object> PostAsync<T>(string link, T content) where T : class;
        Task<object> PutAsync<T>(string link, T content);
        Task<object> DeleteAsync(string link);
    }
}

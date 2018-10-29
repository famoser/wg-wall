using System.Threading.Tasks;

namespace WgWall.Test.Utils.Interface
{
    public interface ITestClient
    {
        Task<string> GetAsync(string link);
        Task<object> GetJsonAsync(string link);
        Task<byte[]> GetFileAsync(string link);
        Task<string> PostFileAsync(string link, byte[] payload, string name = "file");
        Task<object> PostAsync<T>(string link, T content) where T : class;
        Task<object> PutAsync<T>(string link, T content);
        Task<object> DeleteAsync(string link);
    }
}

using RestSharp;

namespace VehicleVortex.Web.Service.IServices
{
    public interface IRestService<T> where T : class
    {
        Task<T> UpdateAsync(string url, T data);
        Task<RestResponse> PostAsync(string url, T data);
        Task<List<T>> GetAllAsync(string url);
        Task<T> GetByIdAsync(string url);
        Task<RestResponse> Delete(string url);

    }
}

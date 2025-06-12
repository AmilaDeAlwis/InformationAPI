using InformationAPI.Models;
using Microsoft.Extensions.Hosting;

namespace InformationAPI.interfaces
{
    public interface IManageService
    {
        Task<InformationModel?> GetInformationByIdAsync(int id);
        Task<(List<InformationModel> Posts, int TotalCount)> GetAllInformationAsync(int page, int pageSize);
    }
}

using InformationAPI.Models;
using Microsoft.Extensions.Hosting;

namespace InformationAPI.interfaces
{
    public interface IManageConnection
    {
        InformationModel? GetById(int id);
        List<InformationModel> GetAll();
        void Insert(InformationModel post);
    }
}

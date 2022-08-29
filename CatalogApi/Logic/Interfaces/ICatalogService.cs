using CatalogApi.Data.Models;
using System.Threading.Tasks;

namespace CatalogApi.Logic.Interfaces
{
    public interface ICatalogService:IBaseService
    {
        Task<CatalogModel> GetById(int Id);
        Task CreateAsync(CatalogModel catalog);
        Task DeleteAsync(int Id);
        Task<CatalogModel> UpdateAsync(CatalogModel catalog);
    }
}

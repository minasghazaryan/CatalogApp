using CatalogApp.Data.Entities;
using CatalogApp.Data.Models;
using System.Threading.Tasks;

namespace CatalogApp.Logic.Interfaces
{
    public interface ICatalogService:IBaseService
    {
        Task<CatalogModel> GetById(int Id);
        Task CreateAsync(CatalogModel catalog);
        Task DeleteAsync(int Id);
        Task<CatalogModel> UpdateAsync(CatalogModel catalog);
    }
}

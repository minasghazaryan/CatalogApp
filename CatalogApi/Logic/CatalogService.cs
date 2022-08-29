using CatalogApi.Data;
using CatalogApi.Data.Entities;
using CatalogApi.Data.Models;
using CatalogApi.Logic.Interfaces;
using CatalogShared;
using CatalogShared.SubPub;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Logic
{
    public class CatalogService : BaseService, ICatalogService
    {
        protected readonly ISubPubService _subPubService;
        public CatalogService( ISubPubService subPubService,AppDbContext context) :base(context)
        {
            _subPubService = subPubService;
        }
        public async Task CreateAsync(CatalogModel catalog)
        {
            var entity = new Catalog
            {
                Name = catalog.Name,
                Image = catalog.Image,
                Coast = catalog.Coast,
                Price = catalog.Price
            };
            try
            {
                await _context.Catalogs.AddAsync(entity);
                await _context.SaveChangesAsync();
                _subPubService.Publish(RabbitChannelType.Email, new CatalogEventMessage { Name = entity.Name, Id = entity.Id ,Price=entity.Price}, "CatalogApp");
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
           
        }



        public async Task DeleteAsync(int Id)
        {
            var catalog = _context.Catalogs.FirstOrDefault(c => c.Id == Id);
            if (catalog != null)
            {
                catalog.Deleted = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<CatalogModel> GetById(int Id)
        {
            var catalog = await _context.Catalogs.
                Where(x => x.Id == Id && !x.Deleted).
                Select(x => new CatalogModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Coast = x.Coast,
                    Image = x.Image
                }).FirstOrDefaultAsync();
            return catalog;
        }

        public async Task<CatalogModel> UpdateAsync(CatalogModel catalog)
        {
            var entity = await _context.Catalogs.Where(x => x.Id == catalog.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.Name = catalog.Name;
                entity.Price = catalog.Price;
                entity.Coast = catalog.Coast;
                entity.Image = catalog.Image;
            }
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return catalog;
        }
    }
}

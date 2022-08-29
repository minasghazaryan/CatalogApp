using CatalogApi.Data;
using CatalogApi.Logic.Interfaces;
using System;
using System.Threading.Tasks;

namespace CatalogApi.Logic
{
    public class BaseService : IBaseService
    {
        protected AppDbContext _context;
        public BaseService(AppDbContext context)
        {
            _context = context;
        }


        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }


        #region IDisposable Members

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

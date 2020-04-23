using Auction_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Repositories
{
    public class UnitOfWork : IDisposable
    {
        AuctionDbContext db = new AuctionDbContext();
        CarsRepository carsRepository;
        CategoryRepository categoryRepository;

        public CarsRepository Cars
        {
            get
            {
                if (carsRepository == null)
                    carsRepository = new CarsRepository(db);
                return carsRepository;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
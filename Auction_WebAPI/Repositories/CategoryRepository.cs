using Auction_WebAPI.Interfaces;
using Auction_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        AuctionDbContext db;

        public CategoryRepository(AuctionDbContext context)
        {
            this.db = context;
        }
        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            var car = db.Categories.Find(id);
            if (car != null)
            {
                db.Categories.Remove(car);
            }
        }

        public void Edit(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }
    }
}
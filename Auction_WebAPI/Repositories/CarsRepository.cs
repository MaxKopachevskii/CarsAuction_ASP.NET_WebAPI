using Auction_WebAPI.Interfaces;
using Auction_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Repositories
{
    public class CarsRepository : IRepository<Car>
    {
        AuctionDbContext db;

        public CarsRepository(AuctionDbContext context)
        {
            this.db = context;
        }

        public void Create(Car item)
        {
            db.Cars.Add(item);
        }

        public void Delete(int id)
        {
            var car = db.Cars.Find(id);
            if (car != null)
            {
                db.Cars.Remove(car);
            }
        }

        public void Edit(Car item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return db.Cars;
        }

        public IEnumerable<Car> GetAllCkeckCars()
        {
            return db.Cars.Where(item => item.IsCheck == true);
        }

        public IEnumerable<Car> GetAllUnCheckCars()
        {
            return db.Cars.Where(item => item.IsCheck == false);
        }

        public IEnumerable<Car> GetAllSedans()
        {
            return db.Cars.Where(item => item.CategoryId == 1);
        }

        public IEnumerable<Car> GetAllCoupe()
        {
            return db.Cars.Where(item => item.CategoryId == 2);
        }

        public IEnumerable<Car> GetAllUniversal()
        {
            return db.Cars.Where(item => item.CategoryId == 3);
        }
    }
}
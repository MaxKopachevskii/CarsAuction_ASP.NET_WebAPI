using Auction_WebAPI.Models;
using Auction_WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auction_WebAPI.Controllers
{
    public class CarsController : ApiController
    {
        UnitOfWork unitOfWork;
        public CarsController()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<Car> GetAll()
        {
            return unitOfWork.Cars.GetAllCkeckCars();
        }

        public Car Get(int id)
        {
            return unitOfWork.Cars.Get(id);
        }

        [HttpPost]
        public void Create([FromBody]Car car)
        {
            unitOfWork.Cars.Create(car);
            unitOfWork.Save();
        }

        [HttpPut]
        public void Edit(int id, [FromBody]Car car)
        {
            unitOfWork.Cars.Edit(car);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.Cars.Delete(id);
            unitOfWork.Save();
        }

        [Route("api/Cars/Sedan")]
        [HttpGet]
        public IEnumerable<Car> Sedan()
        {
            return unitOfWork.Cars.GetAllSedans();
        }

        [Route("api/Cars/Coupe")]
        [HttpGet]
        public IEnumerable<Car> Coupe()
        {
            return unitOfWork.Cars.GetAllCoupe();
        }

        [Route("api/Cars/Universal")]
        [HttpGet]
        public IEnumerable<Car> Universal()
        {
            return unitOfWork.Cars.GetAllUniversal();
        }

        [Route("api/Cars/{id}/{rate}")]
        [HttpPut]
        public void MakeRate(int id,int rate)
        {
            var car = unitOfWork.Cars.Get(id);
            if (car != null)
            {
                if ((rate - car.Price) > 999)
                {
                    car.Price = rate;
                    unitOfWork.Cars.Edit(car);
                    unitOfWork.Save();
                }
            }
        }
    }
}

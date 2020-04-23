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
            return unitOfWork.Cars.GetAll();
        }

        public Car Get(int id)
        {
            return unitOfWork.Cars.Get(id);
        }

        //[Authorize]
        [HttpPost]
        public void Create([FromBody]Car car)
        {
            unitOfWork.Cars.Create(car);
            unitOfWork.Save();
        }

        //[Authorize(Roles = "admin,manager")]
        [HttpPut]
        public void Edit(int id, [FromBody]Car car)
        {
            unitOfWork.Cars.Edit(car);
            unitOfWork.Save();
        }

        //[Authorize(Roles = "admin,manager")]
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
    }
}

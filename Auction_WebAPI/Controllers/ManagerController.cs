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
    public class ManagerController : ApiController
    {
        UnitOfWork unitOfWork;

        public ManagerController()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<Car> GetAllUnchecktedLots()
        {
            return unitOfWork.Cars.GetAllUnCheckCars();
        }


        [Route("api/Manager/{id}/{wasChecked}")]
        [HttpPut]
        public void IsCheck(int id,bool wasChecked)
        {
            var car = unitOfWork.Cars.Get(id);
            if (car != null)
            {
                 car.IsCheck = wasChecked;
                 unitOfWork.Cars.Edit(car);
                 unitOfWork.Save();
            }
        }
    }
}

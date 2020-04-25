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
    //The controller that will be used by the manager to confirm / delete lots
    public class ManagerController : ApiController
    {
        UnitOfWork unitOfWork;

        public ManagerController()
        {
            unitOfWork = new UnitOfWork();
        }

        //Method for receiving all lots that are awaiting confirmation
        public IEnumerable<Car> GetAllUnchecktedLots()
        {
            return unitOfWork.Cars.GetAllUnCheckCars();
        }

        /*The method by which the manager can:
         - confirm the lot and go to the main list of lots (api/Manager/1/true)
         - remove the lot (api/Manager/1/false)*/
        [Route("api/Manager/{id}/{wasChecked}")]
        [HttpPut]
        public HttpResponseMessage IsCheck(int id,bool wasChecked)
        {
            try
            {
                var car = unitOfWork.Cars.Get(id);
                if (car != null && wasChecked == true)
                {
                    car.IsCheck = wasChecked;
                    unitOfWork.Cars.Edit(car);
                    unitOfWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                if (car != null && wasChecked == false)
                {
                    unitOfWork.Cars.Delete(id);
                    unitOfWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

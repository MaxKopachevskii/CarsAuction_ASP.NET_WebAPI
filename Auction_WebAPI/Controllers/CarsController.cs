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
    //Controller for interaction with cars
    public class CarsController : ApiController
    {
        UnitOfWork unitOfWork;
        public CarsController()
        {
            unitOfWork = new UnitOfWork();
        }
        
        //Get all list of cars
        public IEnumerable<Car> GetAll()
        {
            return unitOfWork.Cars.GetAllCkeckCars();
        }

        //Get car with id №
        public HttpResponseMessage Get(int id)
        {
            var car = unitOfWork.Cars.Get(id);
            if (car != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id + " not found");
            }
        }

        //Create new car
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Create([FromBody]Car car)
        {
            try
            {
                unitOfWork.Cars.Create(car);
                unitOfWork.Save();

                var message = Request.CreateResponse(HttpStatusCode.Created, car);
                message.Headers.Location = new Uri(Request.RequestUri + car.Id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Edit car
        [Authorize(Roles = "admin,manager")]
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]Car car)
        {
            try
            {
                unitOfWork.Cars.Edit(car);
                unitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpPut]
        //public HttpResponseMessage Edit(int id, [FromBody]Car car)
        //{
        //    try
        //    {
        //        var _car = unitOfWork.Cars.Get(id);
        //        if (_car != null)
        //        {
        //            unitOfWork.Cars.Edit(car);
        //            unitOfWork.Save();
        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id + "not found to edit");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        //Delete car with id №
        [Authorize(Roles = "admin,manager")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var car = unitOfWork.Cars.Get(id);
                if (car != null)
                {
                    unitOfWork.Cars.Delete(id);
                    unitOfWork.Save();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Car with id = " + id + " not found to delete");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //Get all cars from sedan category
        [Route("api/Cars/Sedan")]
        [HttpGet]
        public IEnumerable<Car> Sedan()
        {
            return unitOfWork.Cars.GetAllSedans();
        }

        //Get all cars from coupe category
        [Route("api/Cars/Coupe")]
        [HttpGet]
        public IEnumerable<Car> Coupe()
        {
            return unitOfWork.Cars.GetAllCoupe();
        }

        //Get all cars from universal category
        [Route("api/Cars/Universal")]
        [HttpGet]
        public IEnumerable<Car> Universal()
        {
            return unitOfWork.Cars.GetAllUniversal();
        }

        //The method allows you to bet on the lot
        [Authorize]
        [Route("api/Cars/{id}/{rate}")]
        [HttpPut]
        public HttpResponseMessage MakeRate(int id,int rate)
        {
            try
            {
                var car = unitOfWork.Cars.Get(id);
                if (car != null)
                {
                    if ((rate - car.Price) > 999)
                    {
                        car.Price = rate;
                        unitOfWork.Cars.Edit(car);
                        unitOfWork.Save();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Rate can't be less then 1000$");
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

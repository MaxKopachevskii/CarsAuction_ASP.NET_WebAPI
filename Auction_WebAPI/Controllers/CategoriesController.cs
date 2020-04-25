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
    //Controller for interaction with categories
    public class CategoriesController : ApiController
    {
        UnitOfWork unitOfWork;
        public CategoriesController()
        {
            unitOfWork = new UnitOfWork();
        }

        //Get all categories
        public IEnumerable<Category> GetAll()
        {
            return unitOfWork.Categories.GetAll();
        }

        //Get category with id №
        public HttpResponseMessage Get(int id)
        {
            var category = unitOfWork.Categories.Get(id);
            if (category != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id = " + id + " not found");
            }
        }

        //Create new category
        [HttpPost]
        public HttpResponseMessage Create([FromBody]Category category)
        {
            try
            {
                unitOfWork.Categories.Create(category);
                unitOfWork.Save();
                var message = Request.CreateResponse(HttpStatusCode.OK, category);
                message.Headers.Location = new Uri(Request.RequestUri + category.Id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Edit category
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]Category category)
        {
            try
            {
                unitOfWork.Categories.Edit(category);
                unitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

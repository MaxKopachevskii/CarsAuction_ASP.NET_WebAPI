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
    public class CategoriesController : ApiController
    {
        UnitOfWork unitOfWork;
        public CategoriesController()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<Category> GetAll()
        {
            return unitOfWork.Categories.GetAll();
        }

        public Category Get(int id)
        {
            return unitOfWork.Categories.Get(id);
        }

        [HttpPost]
        public void Create([FromBody]Category category)
        {
            unitOfWork.Categories.Create(category);
            unitOfWork.Save();
        }

        [HttpPut]
        public void Edit(int id, [FromBody]Category category)
        {
            unitOfWork.Categories.Edit(category);
            unitOfWork.Save();
        }

        //public void Delete(int id)
        //{
        //    unitOfWork.Categories.Delete(id);
        //    unitOfWork.Save();
        //}
    }
}

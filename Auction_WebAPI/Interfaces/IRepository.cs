using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Interfaces
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Edit(T item);
        void Delete(int id);
    }
}
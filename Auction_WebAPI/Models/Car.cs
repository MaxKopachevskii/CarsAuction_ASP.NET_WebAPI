using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public bool IsCheck { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }

        public Car()
        {
            Date = DateTime.Now;
            IsCheck = false;
        }
    }
}
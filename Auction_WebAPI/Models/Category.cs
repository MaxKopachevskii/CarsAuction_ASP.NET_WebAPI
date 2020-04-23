using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction_WebAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }

        public Category()
        {
            Cars = new List<Car>();
        }
    }
}
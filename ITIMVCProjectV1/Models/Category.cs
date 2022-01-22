using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
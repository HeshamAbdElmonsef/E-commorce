using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIMVCProjectV1.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Salary { get; set; }

        public string Image { get; set; }
        public int Amount { get; set; }
        public int TotleAmount { get; set; }
        public string ProviderName { get; set; }
      
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }

        public ICollection<SubOrder> SubOrders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
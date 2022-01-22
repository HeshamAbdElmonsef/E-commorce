using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ITIMVCProjectV1.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
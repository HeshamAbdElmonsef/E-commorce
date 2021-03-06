using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ITIMVCProjectV1.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Column(TypeName = "datetime2")]

        public DateTime ResevationDate { get; set; }
        [Column(TypeName = "datetime2")]

        public DateTime DeliveryDate { get; set; }
        public double Cost { get; set; }
        public string destination { get; set; }

        public bool IsConfirmed { get; set; }

        [ForeignKey("Customer")]
        public int Customer_id { get; set; }
        public Customer Customer { get; set; }
        public ICollection<SubOrder> SubOrders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.ViewModel
{
    public class CustomerConfirmViewModel
    {
        // Order_id ,  ReservationDate DelveryDate Cost Destination
        public int Order_id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DelveryDate { get; set; }
        public Double Cost { get; set; }
        public string Destination { get; set; }

    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.ViewModel
{
    public class CustomerAddComment
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public String Comment { get; set; }
        public int Rate { get; set; }



    }
}
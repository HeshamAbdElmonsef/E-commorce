using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.ViewModel
{
    public class ProductHomePageViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Salary { get; set; }
        public string Image { get; set;}
        public String ProviderName { get; set; }

        public String CategoryType { get; set; }
    }
}
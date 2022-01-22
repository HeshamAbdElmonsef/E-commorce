using ITIMVCProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.ViewModel
{
    public class AdminProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Salary { get; set; }

        public string Image { get; set; }
        public int Amount { get; set; }
        public int TotleAmount { get; set; }
        public string ProviderName { get; set; }

        public int Category_id { get; set; }
        public Category Category { get; set; }

    }
}
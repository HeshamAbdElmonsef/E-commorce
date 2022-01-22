using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITIMVCProjectV1.ViewModel
{
    public class ProductProcessViewModel
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Salary { get; set; }
        [Required]
        public HttpPostedFileBase UploadedFile
        {
            get; set;
        }

        public int TotleAmount { get; set; }
        public string ProviderName { get; set; }

        public int Category_id { get; set; }
    }
}
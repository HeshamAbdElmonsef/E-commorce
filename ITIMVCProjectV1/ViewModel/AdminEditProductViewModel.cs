using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITIMVCProjectV1.ViewModel
{
    public class AdminEditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Salary { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase UploadedFile
        {
            get; set;
        }

        public int TotleAmount { get; set; }
        public string ProviderName { get; set; }

        public int Category_id { get; set; }
    }
}
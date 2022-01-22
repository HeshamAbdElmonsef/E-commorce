using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ITIMVCProjectV1.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
    }
}
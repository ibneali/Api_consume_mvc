using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apicrud.Models
{
    public class empclass
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Country { get; set; }
        public string Salary { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
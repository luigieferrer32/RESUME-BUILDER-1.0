using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ResumeBuilder.ViewModel
{
    public class PersonInfoVM
    {
        public int IDPers { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string State_Province { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string Email_Address { get; set; }
    }
}
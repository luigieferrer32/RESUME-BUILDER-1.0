using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ResumeBuilder.ViewModel
{
    public class EducationVM
    {
        public int ID { get; set; }
        public string School_Name { get; set; }
        public string School_Location { get; set; }
        public string Degree { get; set; }
        public string Field_Of_Study { get; set; }
        public string Graduation_Year { get; set; }

        public List<SelectListItem> ListofDegree { get; set; }
    }
}
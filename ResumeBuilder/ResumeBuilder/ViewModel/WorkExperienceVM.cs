using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ResumeBuilder.ViewModel
{
    public class WorkExperienceVM
    {
        public int Id { get; set; }
        public string Job_Title { get; set; }
        public string Employer { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Start_Month { get; set; }
        public string Start_Year{ get; set; }
        public string End_Month { get; set; }
        public string End_Year { get; set; }
        public string Description { get; set; }

        public List<SelectListItem> ListStartMonth { get; set; }
        public List<SelectListItem> ListStartYear { get; set; }
        public List<SelectListItem> ListEndMonth { get; set; }
        public List<SelectListItem> ListEndYear { get; set; }
      
      

        
    }
}
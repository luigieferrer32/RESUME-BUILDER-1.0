using AutoMapper.QueryableExtensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResumeBuilder.Models;
using DataAccess;
using ResumeBuilder.ViewModel;
using System.Globalization;
using DataAccess.Repositories;

namespace ResumeBuilder.Controllers
{
    public class ResumeController : Controller
    {
         

        private readonly IResumeRepo resumeRepo;

        public ResumeController(IResumeRepo resumeRepository)
        {
            this.resumeRepo = resumeRepository;
        }
      
        // GET: Resume
        [HttpGet]
        public ActionResult Resume()
        {
          
                string CurUserid = Session["CurUserid"].ToString();
          
          
                Session["Userid"] = CurUserid;
                return View();
        
        }

        //public JsonResult UpdateAssistance(UserViewModel model)
        // {

        //     UserMapper userMapper = new UserMapper();
        //     UserRepo userRepo = new UserRepo();



        //     var UpdatedModel = userRepo.Update(userMapper.UserViewModelToResume(model));
        //     return Json(new { UpdatedModel }, JsonRequestBehavior.AllowGet);
        [HttpPost]
        [ActionName("Resume")]
        public ActionResult PersonnalInformation(PersonInfoVM person)
        {
            if (ModelState.IsValid)
            {

                //person.ID = (string)Session["Userid"];
                Mapper.Reset();
                AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<PersonInfoVM, USER>());
                USER userEntity = AutoMapper.Mapper.Map<USER>(person);
                 userEntity.ID  = Session["Userid"].ToString();
                bool result = resumeRepo.AddPersonalInformation(userEntity);

                if (result)
                {
                    Session["IdSelected"] = resumeRepo.GetUserID(person.First_Name, person.Last_Name);
                    
                    //ViewBag.Message("Personal Information added successfully");
                    return RedirectToAction("WorkExperience");

                }
                else
                {
                    ViewBag.Message("Something Wrong!");
                    return View(person);
                }
            }
            ViewBag.MessageForm = "Please Check your form before submit !";
            return View(person);
        }


        [HttpGet]
        public ActionResult WorkExperience()
        {
            return View();
        }


        public PartialViewResult WorkExperiencePartial(WorkExperienceVM workexperience)
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            List<SelectListItem> StartDate = new List<SelectListItem>();
            List<SelectListItem> StartYear = new List<SelectListItem>();
            List<SelectListItem> EndDate = new List<SelectListItem>();
            List<SelectListItem> EndYear = new List<SelectListItem>();
            var dateNow = DateTime.Now.Year - 100;

            for (int i=0; i < months.Length; i++)
            {
                StartDate.Add(new SelectListItem() { Text = months[i].ToString()});
                EndDate.Add(new SelectListItem() { Text = months[i].ToString() });
            }

            for (int i = dateNow; i <= dateNow + 100; i++)
            {
                StartYear.Add(new SelectListItem() { Text = i.ToString() });
                EndYear.Add(new SelectListItem() { Text = i.ToString() });
            }

            workexperience.ListEndMonth = EndDate;
            workexperience.ListEndYear = EndYear;
            workexperience.ListStartMonth = StartDate;
            workexperience.ListStartYear = StartYear;
            return PartialView("~/Views/Resume/ResumeViews/MyWorkExperience.cshtml", workexperience);
        }


        public ActionResult AddOrUpdateExperience(WorkExperienceVM workexperience)
        {
            string msg = string.Empty;
            
            if (workexperience != null)
            {
                AutoMapper.Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<WorkExperienceVM, WORKEXPERIENCE>());
                WORKEXPERIENCE workExperienceEntity = AutoMapper.Mapper.Map<WORKEXPERIENCE>(workexperience);
                int idPer = (int)Session["IdSelected"];

                msg = resumeRepo.AddOrUpdateExperience(workExperienceEntity, idPer);
            }
            else
            {
                msg = "Please re try the operation";
            }
            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Education(EducationVM education)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateEducation(EducationVM education)
        {
            string msg = string.Empty;

            if (education != null)
            {
                //Creating Mapping
                AutoMapper.Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<EducationVM, EDUCATION>());
                EDUCATION educationEntity = Mapper.Map<EDUCATION>(education);

                int idPer = (int)Session["IdSelected"];

                msg = resumeRepo.AddorUpdateEducation(educationEntity, idPer);

            }
            else
            {
                msg = "Please re try the operation";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EducationPartial(EducationVM education)
        {
            List<SelectListItem> ListofDegree = new List<SelectListItem>()
            {
                new SelectListItem {Text = "High School Diploma", Value = "High School Diploma" },
                new SelectListItem { Text = "Diploma", Value = "Diploma"},
                new SelectListItem { Text = "Bachelor's degree", Value = "Bachelor's degree"},
                new SelectListItem { Text = "Master's degree", Value = "Master's degree"},
                new SelectListItem { Text = "Doctorate", Value = "Doctorate"},
            };

            education.ListofDegree = ListofDegree;
            return PartialView("~/Views/Resume/ResumeViews/MyEducation.cshtml", education);
        }

        [HttpGet]
        public ActionResult Skills()
        {
            return View();
        }

        public PartialViewResult SkillsPartial()
        {
            return PartialView("~/Views/Shared/_MySkills.cshtml");
        }

        public ActionResult AddSkill(SkillsVM skill)
        {
            int idPer = (int)Session["IdSelected"];
            string msg = string.Empty;


            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<SkillsVM, SKILL>());
            SKILL skillEntity = AutoMapper.Mapper.Map<SKILL>(skill);

            if (resumeRepo.AddSkill(skillEntity, idPer))
            {
                msg = "skill added Successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CV()
        {
            return View();
        }

        public PartialViewResult GetPersonnalInfoPartial()
        {
            int idPer = (int)Session["IdSelected"];
            USER user = resumeRepo.GetPersonnalInfo(idPer);

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<USER, PersonInfoVM>());
            PersonInfoVM personVM = AutoMapper.Mapper.Map<PersonInfoVM>(user);

            return PartialView("~/Views/Shared/_MyPersonnalInfo.cshtml", personVM);
        }

        public PartialViewResult GetEducationCVPartial()
        {
            int idPer = (int)Session["IdSelected"];

            //Creating Mapping
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<EDUCATION, EducationVM>());
            IQueryable<EducationVM> educationList =  resumeRepo.GetEducationById(idPer).ProjectTo<EducationVM>().AsQueryable();

            return PartialView("~/Views/Shared/_MyEducationCV.cshtml", educationList);
        }

        public PartialViewResult WorkExperienceCVPartial()
        {
            int idPer = (int)Session["IdSelected"];

            //Creating Mapping
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<WORKEXPERIENCE, WorkExperienceVM>());
            IQueryable<WorkExperienceVM> workExperienceList = resumeRepo.GetWorkExperienceById(idPer).ProjectTo<WorkExperienceVM>().AsQueryable();

            return PartialView("~/Views/Shared/_WorkExperienceCV.cshtml", workExperienceList);
        }


    }
}


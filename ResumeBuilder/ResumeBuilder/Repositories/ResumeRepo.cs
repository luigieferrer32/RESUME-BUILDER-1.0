using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AutoMapper.QueryableExtensions;
using System.Data.Entity.Validation;
using System.Data.Entity;
using ResumeBuilder.Controllers;
using ResumeBuilder.Models;

namespace DataAccess.Repositories
{
    public class ResumeRepository : IResumeRepo
    {
        private readonly ResumeBuilderEntities context = new ResumeBuilderEntities();
        public bool AddEducation(EDUCATION education, int idPer)
        {
            try
            {
                int countRecords = 0;
                USER userEntity = context.USERs.Find(idPer);
                if (userEntity != null && education != null)
                {
                    userEntity.EDUCATIONs.Add(education);
                    countRecords = context.SaveChanges();
                }
                return countRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException edEx)
            {
                throw;
            }
        }

        public string AddorUpdateEducation(EDUCATION education, int idUser)
        {
            string msg = string.Empty;

            USER userEntity = context.USERs.Find(idUser);
            if (userEntity != null)
            {
                if (education.ID > 0)
                {
                    context.Entry(education).State = EntityState.Modified;
                    context.SaveChanges();
                    msg = "Education entity has been updated successfully";
                }
                else
                {
                    userEntity.EDUCATIONs.Add(education);
                    context.SaveChanges();
                    msg = "Education entity has been Added successfully";
                }
            }
            return msg;
        }

        public string AddOrUpdateExperience(WORKEXPERIENCE workExperience, int idPer)
        {
           string msg = string.Empty;

            USER userEntity = context.USERs.Find(idPer);
            if(userEntity != null)
            {
                if(workExperience.ID > 0)
                {
                    context.Entry(workExperience).State = EntityState.Modified;
                    context.SaveChanges();

                    msg = "Education entity has been updated successfully";
                }
                else
                {
                    userEntity.WORKEXPERIENCEs.Add(workExperience);
                    context.SaveChanges();
                }
           
            }
            return msg;
        }

        public bool AddPersonalInformation(USER user)
        {
            try
            {
                int nbRecords = 0;
                if (user != null)
                {
                    context.USERs.Add(user);
                    nbRecords = context.SaveChanges();
                };
                return nbRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                        validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
        public bool AddSkill(SKILL skill, int idPer)
        {
                int countRecords = 0;
                USER userEntity = context.USERs.Find(idPer);
                if (userEntity != null && skill != null)
                {
                    userEntity.SKILLS.Add(skill);
                    countRecords = context.SaveChanges();
                }

                return countRecords > 0 ? true : false;
            }

        public IQueryable<EDUCATION> GetEducationById(int idPer)
        {
            var educationList = context.EDUCATIONs.Where(e => e.ID == idPer);
            return educationList;
        }

        public USER GetPersonnalInfo(int idUser)
        {
            return context.USERs.Find(idUser);
        }

        public string GetUserIdbyEmail(string email)
        {
            string idSelected = context.AspNetUsers.Where(e => e.Email.ToLower().Equals(email.ToLower()))
                                                            .Select(e => e.Id).FirstOrDefault();

            return idSelected;
        }

        public IQueryable<SKILL> GetSkillsById(int idPer)
        {
            var skillsList = context.SKILLS.Where(x => x.ID == idPer);
            return skillsList;
        }

        public int GetUserID(string firstName, string lastName)
        {
            int idSelected = context.USERs.Where(u => u.FIRST_NAME.ToLower().Equals(firstName.ToLower()))
                                           .Where(u => u.LAST_NAME.ToLower().Equals(lastName.ToLower()))
                                           .Select(u => u.USER_ID).FirstOrDefault();
            return idSelected;
        }

        public IQueryable<WORKEXPERIENCE> GetWorkExperienceById(int idPer)
        {
            var workExperienceList = context.WORKEXPERIENCEs.Where(w => w.ID == idPer);
            return workExperienceList;
        }
    }
}

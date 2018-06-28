using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResumeBuilder.Models;


namespace DataAccess.Repositories
{
    public interface IResumeRepo
    {
        bool AddPersonalInformation(USER user);
        string AddorUpdateEducation(EDUCATION education, int idUser);
        int GetUserID(string firstName, string lastName);
        string AddOrUpdateExperience(WORKEXPERIENCE workExperience, int idPer);
        bool AddSkill(SKILL skill, int idPer);
        bool AddEducation(EDUCATION education, int idPer);
        USER GetPersonnalInfo(int idUser);
        IQueryable<EDUCATION> GetEducationById(int idPer);
        IQueryable<WORKEXPERIENCE> GetWorkExperienceById(int idPer);
        IQueryable<SKILL> GetSkillsById(int idPer);
    }
}

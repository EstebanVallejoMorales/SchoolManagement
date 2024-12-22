using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Presenters.Presenters
{
    public class TeacherPresenter: IPresenter<Entities.Teacher, TeacherViewModel>
    {
        public IEnumerable<TeacherViewModel> Present(IEnumerable<Entities.Teacher> Teachers)
        {
            return Teachers.Select(Teacher => new TeacherViewModel
            {
                Id = Teacher.Id,
                GenderId = Teacher.GenderId,
                LastName = Teacher.LastName,
                Name = Teacher.Name,
                GenderName = Teacher.Gender.Name,
            });
        }

        public TeacherViewModel? Present(Entities.Teacher? Teacher)
        {
            if (Teacher == null)
            {
                return null;
            }
            return new TeacherViewModel
            {
                Id = Teacher.Id,
                GenderId = Teacher.GenderId,
                LastName = Teacher.LastName,
                Name = Teacher.Name,
                GenderName = Teacher.Gender.Name
            };
        }
    }
}

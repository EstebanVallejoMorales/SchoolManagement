using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Presenters.Presenters
{
    public class StudentPresenter : IPresenter<Entities.Student, StudentViewModel>
    {
        public IEnumerable<StudentViewModel> Present(IEnumerable<Entities.Student> students)
        {
            return students.Select(student => new StudentViewModel 
            {
                Id = student.Id,
                GenderId = student.GenderId,                
                LastName = student.LastName,
                Name = student.Name,
                GenderName = student.Gender.Name
            });
        }

        public StudentViewModel? Present(Entities.Student? student)
        {
            if (student == null)
            {
                return null;
            }
            return new StudentViewModel
            {
                Id = student.Id,
                GenderId = student.GenderId,
                LastName = student.LastName,
                Name = student.Name,
                GenderName = student.Gender.Name
            };
        }
    }
}

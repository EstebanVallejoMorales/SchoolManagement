using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Presenters.Presenters
{
    public class GradePresenter : IPresenter<Grade, GradeViewModel>
    {
        public IEnumerable<GradeViewModel> Present(IEnumerable<Grade> Grades)
        {
            return Grades.Select(student => new GradeViewModel
            {
                Id = student.Id,
                Name = student.Name
            });
        }

        public GradeViewModel? Present(Grade? grade)
        {
            if (grade == null)
            {
                return null;
            }
            return new GradeViewModel
            {
                Id = grade.Id,
                Name = grade.Name,
            };
        }
    }
}

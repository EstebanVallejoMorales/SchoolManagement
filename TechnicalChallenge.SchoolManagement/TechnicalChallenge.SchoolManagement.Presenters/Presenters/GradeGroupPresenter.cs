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
    public class GradeGroupPresenter: IPresenter<GradeGroup, GradeGroupViewModel>
    {
        public IEnumerable<GradeGroupViewModel> Present(IEnumerable<GradeGroup> gradeGroups)
        {
            return gradeGroups.Select(gradeGroup => new GradeGroupViewModel
            {
                Id = gradeGroup.Id,
                Name = $"{gradeGroup.Grade.Name} {gradeGroup.Group.Name}"
            });
        }

        public GradeGroupViewModel? Present(GradeGroup? gradeGroup)
        {
            if (gradeGroup == null)
            {
                return null;
            }
            return new GradeGroupViewModel
            {
                Id = gradeGroup.Id,
                Name = $"{gradeGroup.Grade.Name} {gradeGroup.Group.Name}"
            };
        }
    }
}

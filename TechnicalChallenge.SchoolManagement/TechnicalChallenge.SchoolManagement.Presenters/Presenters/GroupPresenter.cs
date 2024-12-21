using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Presenters.Presenters
{
    public class GroupPresenter : IPresenter<Entities.Group, GroupViewModel>
    {
        public IEnumerable<GroupViewModel> Present(IEnumerable<Group> groups)
        {
            return groups.Select(student => new GroupViewModel
            {
                Id = student.Id,                
                Name = student.Name                
            });
        }

        public GroupViewModel? Present(Group? group)
        {
            if (group == null)
            {
                return null;
            }
            return new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
            };
        }
    }
}

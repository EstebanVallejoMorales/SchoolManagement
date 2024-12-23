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
    public class GenderPresenter : IPresenter<Gender, GenderViewModel>
    {
        public IEnumerable<GenderViewModel> Present(IEnumerable<Gender> genders)
        {
            return genders.Select(gender => new GenderViewModel
            {
                Id = gender.Id,
                Name = gender.Name
            });
        }

        public GenderViewModel? Present(Gender? gender)
        {
            if (gender == null)
            {
                return null;
            }
            return new GenderViewModel
            {
                Id = gender.Id,
                Name = gender.Name,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.UseCases.Interfaces
{
    public interface IMapper<TDtoInput, TOutput>
    {
        public TOutput ToEntity(TDtoInput dtoInput);
    }
}

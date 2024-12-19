using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Error;

namespace TechnicalChallenge.SchoolManagement.Presenters.ViewModels
{
    public class ResponseDtoViewModel<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}

using FluentValidation;
using TechnicalChallenge.SchoolManagement.Dto.Student;

namespace TechnicalChallenge.SchoolManagement.Api.Validators
{
    public class StudentValidator: AbstractValidator<CreateStudentRequestDto>
    {
        public StudentValidator()
        {
            RuleFor(dto => dto.BirthDate)
                .NotNull().
                NotEmpty().
                GreaterThan(DateTime.Now.AddYears(-120))
                .WithMessage("La fecha de nacimiento no debe ser mayor a 120 años atrás."); ;
        }
    }
}

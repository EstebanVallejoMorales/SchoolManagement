using TechnicalChallenge.SchoolManagement.Entities;

namespace TechnicalChallenge.SchoolManagement.Presenters.Student
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string BirthDate { get; set; }
    }
}

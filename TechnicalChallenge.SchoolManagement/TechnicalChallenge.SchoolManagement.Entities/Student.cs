namespace TechnicalChallenge.SchoolManagement.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string GradeGroupName { get; set; }
    }
}

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GenderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Student relationship
        public ICollection<StudentModel> Students { get; set; } // A gender can be related with several students

        // Teacher relationship
        public ICollection<TeacherModel> Teachers { get; set; }  // A gender can be related with several teachers
    }
}

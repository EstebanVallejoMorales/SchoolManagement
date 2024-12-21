namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GenderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relationships
        public ICollection<StudentModel> Students { get; set; }
        public ICollection<TeacherModel> Teachers { get; set; }
    }
}

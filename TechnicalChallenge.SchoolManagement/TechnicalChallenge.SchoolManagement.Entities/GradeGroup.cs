using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Entities
{
    public class GradeGroup
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}

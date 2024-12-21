using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GradeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relationship: GradeGroup
        public ICollection<GradeGroupModel> GradeGroups { get; set; }
    }
}

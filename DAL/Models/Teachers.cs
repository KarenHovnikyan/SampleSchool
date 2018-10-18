using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        [Required]
        [StringLength(12)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [Range(20, 90, ErrorMessage = "Can only be between 20 .. 90")]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(12)]
        [RegularExpression(@"[0]{1}[1|4|5|6|7|9]{1}[0|1|3|4|5|7|8|9]{1}\ [0-9]{3}\ \d{3}", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public int? SchoolID { get; set; }
        public virtual School School { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public Teacher()
        {
            Subjects = new List<Subject>();
            Date = DateTime.Now;
        }
    }
}

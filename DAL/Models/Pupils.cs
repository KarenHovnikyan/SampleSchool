using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Pupil
    {
        public int PupilID { get; set; }
        [Required]
        [StringLength(12)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [Range(5, 20, ErrorMessage = "Can only be between 5 .. 20")]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(12)]
        [RegularExpression(@"[0]{1}[1]{1}[0]{1}\ [0-9]{3}\ \d{3}", ErrorMessage = "Not a valid phone number")]
        public string HomePhone { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Class")]
        public string SchoolClass { get; set; }
        public DateTime Date { get; set; }

        public int? SchoolID { get; set; }
        public virtual School School { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public Pupil()
        {
            Subjects = new List<Subject>();
            Date = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "SchoolName")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }  

        [Required]
        [StringLength(12)]
        [RegularExpression(@"[0]{1}[1]{1}[0]{1}\ [0-9]{3}\ \d{3}", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public int CountPupil { get; set; }
        public DateTime Date { get; set; }
        
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }

        public bool Valid { get; set; }

        public School()
        {
            Teachers = new List<Teacher>();
            Pupils = new List<Pupil>();
            Date = DateTime.Now;
            Valid = true;
            CountPupil = 0;
        }
    }  
}
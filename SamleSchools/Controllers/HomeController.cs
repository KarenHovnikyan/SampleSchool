using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamleSchools.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return View(unitOfWork.SchoolRepository.GetAll.Where(c => c.Valid == true).ToList());
            }
        }
        public ActionResult Teacher()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return View(unitOfWork.TeacherRepository.GetAll);
            }
        }
        public ActionResult Pupil()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return View(unitOfWork.PupilRepository.GetAll);
            }
        }
        public ActionResult Subject()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return View(unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true));       
            }
        }

        public ActionResult DetailsSchool(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork()) 
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                School school = unitOfWork.SchoolRepository.GetById(id);
                if (school == null)
                {
                    return RedirectToAction("index");
                }
                ViewBag.pupil = unitOfWork.PupilRepository.GetAll.Where(p => p.SchoolID == id).ToList();
                ViewBag.Teacher = unitOfWork.TeacherRepository.GetAll.Where(p => p.SchoolID == id).ToList();
                return View(school);
            }
            
        }

        public ActionResult DetailsTeacher(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);
                if (teacher == null)
                {
                    return RedirectToAction("Teacher");
                }
                IEnumerable<Subject> SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);
                IEnumerable<Subject> teachetSubject = teacher.Subjects.ToList();
               
                List<Pupil> pupilList = new List<Pupil>(); 
                foreach (var p in SubjectAll)
                {
                    foreach (var s in teachetSubject)
                    {
                        if (p.SubjectID == s.SubjectID)
                        {
                            pupilList.AddRange(p.Pupils);
                        }
                           
                    }
                   
                }
                var distinctPupilList = pupilList.GroupBy(p => p.PupilID)
                                          .Select(g => g.First());
                
                ViewBag.pupilList = distinctPupilList;
                ViewBag.Subject = teacher.Subjects.Where(s => s.Valid == true).ToList();

                return View(teacher);
            }
        }

        public ActionResult DetailsPupil(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Pupil pupil = unitOfWork.PupilRepository.GetById(id);
                if (pupil == null)
                {
                    return RedirectToAction("Pupil");
                }
                IEnumerable<Subject> SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);
                IEnumerable<Subject> pupilSubject = pupil.Subjects.ToList();
                
                List<Teacher> teacherList = new List<Teacher>();
                foreach (var p in SubjectAll)
                {
                    foreach (var s in pupilSubject)
                    {
                        if (p.SubjectID == s.SubjectID)
                        {
                            teacherList.AddRange(p.Teachers);
                        }

                    }

                }
                var distinctTeacherList = teacherList.GroupBy(p => p.TeacherID)
                                          .Select(g => g.First());

                ViewBag.teacherList = distinctTeacherList;
                ViewBag.Subject = pupil.Subjects.Where(s => s.Valid == true).ToList();
                return View(pupil);
            }
        }
        
        public ActionResult DetailsSubject(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Subject subject = unitOfWork.SubjectRepository.GetById(id);
                if (subject == null)
                {
                    return RedirectToAction("Pupil");
                }
                ViewBag.Pupil = subject.Pupils.ToList();
                ViewBag.Teacher = subject.Teachers.ToList();

                return View(subject);
            }
        }

        [HttpGet]
        public ActionResult CreateSchool()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSchool(School school)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<School> schoolList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Phone == school.Phone).ToList();
                List<School> schoolList1 = unitOfWork.SchoolRepository.GetAll.Where(s => s.Address == school.Address).ToList();
                if (schoolList.Count != 0)
                {
                    ModelState.AddModelError("Phone", "Such Number already exists");
                }
                if (schoolList1.Count != 0)
                {
                    ModelState.AddModelError("Address", "Such Address already exists");
                }
                if (ModelState.IsValid)
                {
                    unitOfWork.SchoolRepository.Create(school);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View(school);
        }
        [HttpGet]
        public ActionResult CreateTeacher()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                ViewBag.Subjects = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);
            } 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(Teacher teacher,int [] selectedSubjects)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                ViewBag.Subjects = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);
                List<Teacher> teacherList = unitOfWork.TeacherRepository.GetAll.Where(s => s.Phone == teacher.Phone).ToList();
                if (teacherList.Count != 0)
                {
                    ModelState.AddModelError("Phone", "Such Number already exists");
                }

                string phoneStr = teacher.Phone.Substring(1, 2);
                if (phoneStr[0] == '1' && phoneStr[1] != '0' ||
                    phoneStr[0] == '4' && phoneStr[1] != '3' ||
                    phoneStr[0] == '5' && phoneStr[1] != '5' ||
                    phoneStr[0] == '6' && phoneStr[1] != '0' ||
                    phoneStr[0] == '7' && phoneStr[1] != '7' ||
                    phoneStr[0] == '9' && phoneStr[1] == '0' ||
                    phoneStr[1] == '2' || phoneStr[1] == '6' )
                {
                    ModelState.AddModelError("Phone", "Not a valid phone number");
                }
                if (ModelState.IsValid)
                {
                   
                    if (selectedSubjects != null)
                    {
                        foreach (var s in unitOfWork.SubjectRepository.GetAll.Where(c => selectedSubjects.Contains(c.SubjectID)))
                        {
                            teacher.Subjects.Add(s);
                        }
                    }
                    unitOfWork.TeacherRepository.Create(teacher);
                    unitOfWork.Save();
                    return RedirectToAction("Teacher");
                }
                
            }
            return View(teacher);
        }
        [HttpGet]
        public ActionResult CreatePupil()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                ViewBag.Subjects = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePupil(Pupil pupil, int [] selectedSubjects)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                ViewBag.Subjects = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);

                if (selectedSubjects != null)
                {
                    foreach (var s in unitOfWork.SubjectRepository.GetAll.Where(c => selectedSubjects.Contains(c.SubjectID)))
                    {
                        pupil.Subjects.Add(s);                
                    }
                }
                if (ModelState.IsValid)
                {
                    unitOfWork.PupilRepository.Create(pupil);
                    unitOfWork.Save();
                    return RedirectToAction("Pupil");
                }
            }
            return View(pupil);
        }

        [HttpGet]
        public ActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSubject(Subject subject)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<Subject> subjectList = unitOfWork.SubjectRepository.GetAll.Where(s => s.Name == subject.Name).ToList();
                if (subjectList.Count != 0)
                {
                    ModelState.AddModelError("Name", "Such Name already exists");
                }
                if (ModelState.IsValid)
                {
                    unitOfWork.SubjectRepository.Create(subject);
                    unitOfWork.Save();
                    return RedirectToAction("Subject");
                }
            }
            return View(subject);
        }

        [HttpGet]
        public ActionResult EditSchool(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                School school = unitOfWork.SchoolRepository.GetById(id);
                if(school != null)
                {
                    return View(school);
                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchool(School school)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<School> schoolList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Phone == school.Phone).ToList();
                List<School> schoolList1 = unitOfWork.SchoolRepository.GetAll.Where(s => s.Address == school.Address).ToList();
                if (schoolList.Count != 1)
                {
                    ModelState.AddModelError("Phone", "Such Number already exists");
                }
                if (schoolList1.Count != 1)
                {
                    ModelState.AddModelError("Address", "Such Number already exists");
                }
                if (ModelState.IsValid)
                {
                    school.Date = DateTime.Now;
                    unitOfWork.SchoolRepository.Update(school);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            return View(school);
        }

        [HttpGet]
        public ActionResult EditTeacher(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {                             
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);

                if (teacher != null)
                {
                    var SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);

                    List<string> subjectAll = new List<string>();
                    List<string> subjectTrue = new List<string>();
                    foreach (var item in SubjectAll)
                    {
                        foreach (var item1 in teacher.Subjects)
                        {
                            if (item.SubjectID == item1.SubjectID)
                                subjectTrue.Add(item.Name);
                        }
                        subjectAll.Add(item.Name);
                    }
                    ViewBag.SubjectAll = subjectAll;
                    ViewBag.SubjectTrue = subjectTrue;

                    return View(teacher);
                }
            }
            return RedirectToAction("Teacher");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeacher(Teacher teacher, string [] selectedSubjects)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<Teacher> teacherList = unitOfWork.TeacherRepository.GetAll.Where(s => s.Phone == teacher.Phone).ToList();
                if (teacherList.Count != 1)
                {
                    ModelState.AddModelError("Phone", "Such Number already exists");
                }
                var SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);

                List<string> subjectAll = new List<string>();
                List<string> subjectTrue = new List<string>();
                foreach (var item in SubjectAll)
                {
                    foreach (var item1 in teacher.Subjects)
                    {
                        if (item.SubjectID == item1.SubjectID)
                            subjectTrue.Add(item.Name);
                    }
                    subjectAll.Add(item.Name);
                }
                ViewBag.SubjectAll = subjectAll;
                ViewBag.SubjectTrue = subjectTrue;

                if (ModelState.IsValid)
                {
                    Teacher newTeacher = unitOfWork.TeacherRepository.GetById(teacher.TeacherID);
                    newTeacher.FirstName = teacher.FirstName;
                    newTeacher.LastName = teacher.LastName;
                    newTeacher.Age = teacher.Age;
                    newTeacher.Address = teacher.Address;
                    newTeacher.Phone = teacher.Phone;
                    newTeacher.SchoolID = teacher.SchoolID;

                    newTeacher.Subjects.Clear();
                    if (selectedSubjects != null)
                    {
                        foreach (var c in unitOfWork.SubjectRepository.GetAll.Where(co => selectedSubjects.Contains(co.Name)))
                        {
                            newTeacher.Subjects.Add(c);
                        }

                    }

                    teacher.Date = DateTime.Now;
                    unitOfWork.TeacherRepository.Update(newTeacher);
                    unitOfWork.Save();
                    return RedirectToAction("Teacher");
                }

                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
            }
            return View(teacher);
        }

        [HttpGet]
        public ActionResult EditPupil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
                Pupil pupil = unitOfWork.PupilRepository.GetById(id);

                if (pupil != null)
                {
                    var SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);

                    List<string> subjectAll = new List<string>();
                    List<string> subjectTrue = new List<string>();
                    foreach (var item in SubjectAll)
                    {
                        foreach (var item1 in pupil.Subjects)
                        {
                            if (item.SubjectID == item1.SubjectID)
                                subjectTrue.Add(item.Name);
                        }
                        subjectAll.Add(item.Name);
                    }
                    ViewBag.SubjectAll = subjectAll;
                    ViewBag.SubjectTrue = subjectTrue;

                    return View(pupil);
                }
            }
            return RedirectToAction("Pupil");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPupil(Pupil pupil, string [] selectedSubjects)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var SubjectAll = unitOfWork.SubjectRepository.GetAll.Where(s => s.Valid == true);

                List<string> subjectAll = new List<string>();
                List<string> subjectTrue = new List<string>();
                foreach (var item in SubjectAll)
                {
                    foreach (var item1 in pupil.Subjects)
                    {
                        if (item.SubjectID == item1.SubjectID)
                            subjectTrue.Add(item.Name);
                    }
                    subjectAll.Add(item.Name);
                }
                ViewBag.SubjectAll = subjectAll;
                ViewBag.SubjectTrue = subjectTrue;

                if (ModelState.IsValid)
                {
                    Pupil newPupil = unitOfWork.PupilRepository.GetById(pupil.PupilID);
                    newPupil.FirstName = pupil.FirstName;
                    newPupil.LastName = pupil.LastName;
                    newPupil.Age = pupil.Age;
                    newPupil.Address = pupil.Address;
                    newPupil.HomePhone = pupil.HomePhone;
                    newPupil.SchoolID = pupil.SchoolID;

                    newPupil.Subjects.Clear();
                    if (selectedSubjects != null)
                    {
                        foreach (var c in unitOfWork.SubjectRepository.GetAll.Where(co => selectedSubjects.Contains(co.Name)))
                        {
                            newPupil.Subjects.Add(c);
                        }

                    }

                    pupil.Date = DateTime.Now;
                    unitOfWork.PupilRepository.Update(newPupil);
                    unitOfWork.Save();
                    return RedirectToAction("Pupil");
                }

                ViewBag.selectList = unitOfWork.SchoolRepository.GetAll.Where(s => s.Valid == true);
            }
            return View(pupil);
        }

        public ActionResult DeleteSchool (int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                School school = unitOfWork.SchoolRepository.GetById(id);
                IEnumerable<Teacher> teachers = unitOfWork.TeacherRepository.GetAll.Where(t => t.SchoolID == id).ToList();
                IEnumerable<Pupil> pupils = unitOfWork.PupilRepository.GetAll.Where(p => p.SchoolID == id).ToList();

                if ( teachers.Count() == 0 && pupils.Count() == 0)
                {
                    unitOfWork.SchoolRepository.Delete(school);
                    unitOfWork.Save();
                }
                else
                {
                    school.Valid = false;
                    unitOfWork.SchoolRepository.Update(school);
                    unitOfWork.Save();
                }
               
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTeacher(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);
                unitOfWork.TeacherRepository.Delete(teacher);
                unitOfWork.Save();
            }
            return RedirectToAction("Teacher");
        }

        public ActionResult DeletePupil(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Pupil pupil = unitOfWork.PupilRepository.GetById(id);

                unitOfWork.PupilRepository.Delete(pupil);
                unitOfWork.Save();
            }
            return RedirectToAction("Pupil");
        }
        public ActionResult DeleteSubject(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Subject subject = unitOfWork.SubjectRepository.GetById(id);
                subject.Valid = false;
                unitOfWork.SubjectRepository.Update(subject);
                unitOfWork.Save();
            }
            return RedirectToAction("Subject");
        }
    }
}
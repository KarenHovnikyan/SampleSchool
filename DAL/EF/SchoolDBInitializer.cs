using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    public class SchoolDBInitializer : DropCreateDatabaseAlways<SchoolContext>
    {
        protected override void Seed(SchoolContext db)
        {

            School sc1 = new School { Name = "Manuk Abexyani - N3", Address = "9 Khanjyan St,Yerevan 00010", Phone = "010 251 415", CountPupil = 10 };
            School sc2 = new School { Name = "N - 181", Address = "4 Arno babajanyan St,Yerevan ", Phone = "010 251 416", CountPupil = 4 };
            db.Schools.AddRange(new List<School> { sc1, sc2 });
            db.SaveChanges();

            Subject s1 = new Subject { Name = "Mathematics" };
            Subject s2 = new Subject { Name = "Physics" };
            Subject s3 = new Subject { Name = "Music" };
            Subject s4 = new Subject { Name = "History" };
            Subject s5 = new Subject { Name = "English" };
            Subject s6 = new Subject { Name = "Dance" };
            Subject s7 = new Subject { Name = "Art" };
            Subject s8 = new Subject { Name = "Chemistry" };
            Subject s9 = new Subject { Name = "Commerce" };
            Subject s10 = new Subject { Name = "Design technology" };
            Subject s11 = new Subject { Name = "Literature" };
            db.Subjects.AddRange(new List<Subject> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 });
            db.SaveChanges();

            Teacher t1 = new Teacher { FirstName = "Hakob", LastName = "Asatryan", Age = 45, Address = "Andranik 119/19", Phone = "098 669 669", SchoolID = sc1.SchoolID };
            Teacher t2 = new Teacher { FirstName = "Ani", LastName = "Petrosyan", Age = 29, Address = "Tichina 11/79", Phone = "094 469 339", SchoolID = sc1.SchoolID };
            Teacher t3 = new Teacher { FirstName = "Mika", LastName = "Sahakyan", Age = 35, Address = "Kentron 16/12", Phone = "093 393 621", SchoolID = sc1.SchoolID };
            Teacher t4 = new Teacher { FirstName = "Sasun", LastName = "Vardanyan", Age = 60, Address = "Haxtanak 220/110/10", Phone = "093 123 321", SchoolID = sc1.SchoolID };
            Teacher t5 = new Teacher { FirstName = "Rafo", LastName = "Miqaelyan", Age = 32, Address = "Isaakov 12", Phone = "098 413 377", SchoolID = sc1.SchoolID };
            Teacher t6 = new Teacher { FirstName = "Liza", LastName = "Sukiasyan", Age = 26, Address = "Baxramyan 37", Phone = "055 418 532", SchoolID = sc1.SchoolID };
            Teacher t7 = new Teacher { FirstName = "Meri", LastName = "Manukyan", Age = 50, Address = "Raffu 49/1", Phone = "055 652 324", SchoolID = sc1.SchoolID };
            Teacher t8 = new Teacher { FirstName = "Anush", LastName = "Antonyan", Age = 35, Address = "Khanjyan 36/4", Phone = "098 635 635", SchoolID = sc1.SchoolID };
            Teacher t9 = new Teacher { FirstName = "Ani", LastName = "Vardanyan", Age = 54, Address = "Arshakunyac 45", Phone = "093 271 123", SchoolID = sc2.SchoolID };
            Teacher t10 = new Teacher { FirstName = "Vika", LastName = "Xachatryan", Age = 20, Address = "Kievyan 2/1", Phone = "055 217 894", SchoolID = sc2.SchoolID };
            db.Teachers.AddRange(new List<Teacher> { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10 });
            db.SaveChanges();

            Pupil p1 = new Pupil { FirstName = "Ani", LastName = "Asatryan", Age = 15, Address = "Andranik 11/11", HomePhone = "010 365 365", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p2 = new Pupil { FirstName = "Aram", LastName = "Abrahamyan", Age = 15, Address = "Andranik 110/19", HomePhone = "010 452 568", SchoolClass = "10b", SchoolID = sc1.SchoolID };
            Pupil p3 = new Pupil { FirstName = "Tigran", LastName = "Hovsepyan", Age = 15, Address = "Andranik 110/16", HomePhone = "010 458 695", SchoolClass = "10b", SchoolID = sc1.SchoolID };
            Pupil p4 = new Pupil { FirstName = "Tigran", LastName = "Tumanyan", Age = 15, Address = "Andranik 125/26", HomePhone = "010 125 365", SchoolClass = "10b", SchoolID = sc1.SchoolID };
            Pupil p5 = new Pupil { FirstName = "Mika", LastName = "Miqaelyan", Age = 15, Address = "Andranik 1/11", HomePhone = "010 458 584", SchoolClass = "10b", SchoolID = sc1.SchoolID };
            Pupil p6 = new Pupil { FirstName = "Astxik", LastName = "Hovhanisyan", Age = 15, Address = "Andranik 26/3", HomePhone = "010 256 254", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p7 = new Pupil { FirstName = "Vigen", LastName = "Sahakyan", Age = 15, Address = "Andranik 100/3", HomePhone = "010 659 965", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p8 = new Pupil { FirstName = "Ani", LastName = "Santrosyan", Age = 15, Address = "Andranik 45/3", HomePhone = "010 964 964", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p9 = new Pupil { FirstName = "Monika", LastName = "Gevorgyan", Age = 15, Address = "Andranik 20/2", HomePhone = "010 111 325", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p10 = new Pupil { FirstName = "Vahan", LastName = "Hovhanisyan", Age = 15, Address = "Andranik 170/91", HomePhone = "010 987 123", SchoolClass = "10a", SchoolID = sc1.SchoolID };
            Pupil p11 = new Pupil { FirstName = "Diyanna", LastName = "vardanyan", Age = 16, Address = "Andranik 100/36", HomePhone = "010 365 124", SchoolClass = "9a", SchoolID = sc2.SchoolID };
            Pupil p12 = new Pupil { FirstName = "Volodia", LastName = "Voskanyan", Age = 14, Address = "Xanjyan 23", HomePhone = "010 254 632", SchoolClass = "9a", SchoolID = sc2.SchoolID };
            Pupil p13 = new Pupil { FirstName = "Voskan", LastName = "Minasyan", Age = 14, Address = "Sayat Nova 1/2", HomePhone = "010 245 325", SchoolClass = "9a", SchoolID = sc2.SchoolID };
            Pupil p14 = new Pupil { FirstName = "Azat", LastName = "Abrahamyan", Age = 14, Address = "Heraci 21/85", HomePhone = "010 694 865", SchoolClass = "9a", SchoolID = sc2.SchoolID };
            db.Pupils.AddRange(new List<Pupil> { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 });

            db.SaveChanges();
            base.Seed(db);
        }
    }
}

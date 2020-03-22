using EnrollmentApplication.Data;
using EnrollmentApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EnrollmentApplication.Controllers
{
    public class StudentController : Controller
    {
        private EnrollmentDB db = new EnrollmentDB();

        public ActionResult StudentOfTheMonth()
        {
            Student student = getStudentOfTheMonth();
            return PartialView("_getStudentOfTheMonth", student);
        }

        /// <summary>
        /// Select the Student of the Month
        /// </summary>
        /// <returns></returns>
        private Student getStudentOfTheMonth()
        {
            return db.Students
                .OrderBy(o => Guid.NewGuid())
                .First();
        }

        public ActionResult StudentSearch(string studentName)
        {
            return PartialView("_studentSearch", getStudent(studentName));
        }

        /// <summary>
        /// Finds the students entered into the search field
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private List<Student> getStudent(string student)
        {
            return db.Students
                .Where(o => o.StudentFirstName.Contains(student) || o.StudentLastName.Contains(student))
                .OrderBy(o => o.StudentLastName)
                .OrderBy(o => o.StudentFirstName).ToList();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,StudentFirstName,StudentLastName,Age,Address1,Address2,City,Zipcode,State")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StudentFirstName,StudentLastName,Age,Address1,Address2,City,Zipcode,State")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

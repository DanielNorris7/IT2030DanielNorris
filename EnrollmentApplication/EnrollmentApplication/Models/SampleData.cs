using EnrollmentApplication.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EnrollmentApplication.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<EnrollmentDB>
    {
        protected override void Seed(EnrollmentDB context)
        {
            List<Course> courses = new List<Course>
            {
                new Course{ CourseTitle = "ASP.Net", CourseDescription = "Web Development using ASP.Net", CourseID = 4},
                new Course{ CourseTitle = "iOS Development", CourseDescription = "iOS Development using Swift", CourseID = 4},
                new Course{ CourseTitle = "Android Development", CourseDescription = "Android Development using Java", CourseID = 5},
                new Course{ CourseTitle = "Machine Learning", CourseDescription = "Using algorithms to build a neural network of decision making", CourseID = 3},
                new Course{ CourseTitle = "Intro to DevOps", CourseDescription = "Exploring DevOps: Jenkins, Git, AWS", CourseID = 4}
            };

            List<Student> students = new List<Student>
            {
                new Student{StudentFirstName = "Daniel", StudentLastName = "Norris" },
                new Student{StudentFirstName = "Molly", StudentLastName = "Norris" },
                new Student{StudentFirstName = "Berkeley", StudentLastName = "Norris" },
                new Student{StudentFirstName = "Tony", StudentLastName = "Stark" },
                new Student{StudentFirstName = "Bruce", StudentLastName = "Wayne" }
            };

            new List<Enrollment>
            {
                new Enrollment{ Grade = "A", Student = students.Single(o => o.StudentID == 1), Course = courses.Single(o => o.CourseID == 5)},
                new Enrollment{ Grade = "B", Student = students.Single(o => o.StudentID == 2), Course = courses.Single(o => o.CourseID == 4)},
                new Enrollment{ Grade = "C-", Student = students.Single(o => o.StudentID == 3), Course = courses.Single(o => o.CourseID == 3)},
                new Enrollment{ Grade = "D+", Student = students.Single(o => o.StudentID == 4), Course = courses.Single(o => o.CourseID == 2)},
                new Enrollment{ Grade = "A+", Student = students.Single(o => o.StudentID == 5), Course = courses.Single(o => o.CourseID == 1)},
                new Enrollment{ Grade = "A-", Student = students.Single(o => o.StudentID == 5), Course = courses.Single(o => o.CourseID == 4)},
                new Enrollment{ Grade = "B", Student = students.Single(o => o.StudentID == 4), Course = courses.Single(o => o.CourseID == 3)},
                new Enrollment{ Grade = "B+", Student = students.Single(o => o.StudentID == 3), Course = courses.Single(o => o.CourseID == 5)},
                new Enrollment{ Grade = "C", Student = students.Single(o => o.StudentID == 2), Course = courses.Single(o => o.CourseID == 4)},
                new Enrollment{ Grade = "C+", Student = students.Single(o => o.StudentID == 1), Course = courses.Single(o => o.CourseID == 2)},
            }.ForEach(o => context.Enrollments.Add(o));
        }
    }
}
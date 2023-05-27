using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class TeacherController : Controller
    {
        private readonly GradeService _gradeService;
        private readonly SubjectService _subjectService;
        private readonly UserService _userService;

        public TeacherController()
        {
            _gradeService = new GradeService();
            _subjectService = new SubjectService();
            _userService = new UserService();
        }

        public ActionResult ChooseSubject(string userId)
        {
            var model = new TeacherModel()
            {
                UserId = userId,
                Subjects = _subjectService.GetAllSubjectsBelongingToTeacherByUserId(userId), 
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChooseSubject(TeacherModel model)
        {
            return RedirectToAction("Grades", "Teacher", new { userId = model.UserId, subjectId = model.ChoosenSubject.Id });
        }

        public ActionResult Grades(string userId, string subjectId)
        { 
            var model = new TeacherModel()
            {
                UserId = userId,
                Students = _gradeService.GetAllStudentsThatHasTheSubjectBySubjectId(subjectId),
                ChoosenSubject = _subjectService.GetSubjectById(subjectId), 
            };
            return View(model);
        }

        public ActionResult EditGrade(string userId, string studentId, string subjectId)
        {
            TeacherModel model = new()
            {
                UserId = userId,
                ChoosenSubject = _subjectService.GetSubjectById(subjectId),
                EditGrade = new(),
            };

            model.EditGrade.StudentId = studentId; 
            model.EditGrade.Student = _userService.GetUserByStudentId(studentId);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditGrade(TeacherModel model)
        {
            _gradeService.EditGrade(model.EditGrade.StudentId, model.ChoosenSubject.Id, model.EditGrade.Grade.Grade);
            return RedirectToAction("Grades", "Teacher", new { userId = model.UserId, subjectId = model.ChoosenSubject.Id });
        }
    }
}

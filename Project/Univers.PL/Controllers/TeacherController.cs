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

        public TeacherController()
        {
            _gradeService = new GradeService();
            _subjectService = new SubjectService();
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
    }
}

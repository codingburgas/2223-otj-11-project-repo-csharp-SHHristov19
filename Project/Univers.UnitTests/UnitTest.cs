namespace Univers.UnitTests
{
    public class Tests
    {
        private readonly ExamService _examService;
        private readonly FacultyService _facultyService;
        private readonly HolidayService _holidayService;
        private readonly SemesterService _semesterService;
        private readonly SpecialityService _specialityService;
        private readonly StaffService _staffService;
        private readonly StudentCourseService _studentCourseService;
        private readonly StudentService _studentService;
        private readonly SubjectService _subjectService;
        private readonly UniversityService _universityService;
        private readonly UserService _userService;
        private readonly Utilities.Utilities _utilities;

        public Tests()
        {
            _examService = new ExamService();
            _facultyService = new FacultyService();
            _holidayService = new HolidayService();
            _semesterService = new SemesterService();
            _specialityService = new SpecialityService();
            _staffService = new StaffService();
            _studentCourseService = new StudentCourseService();
            _studentService = new StudentService();
            _subjectService = new SubjectService();
            _universityService = new UniversityService();
            _userService = new UserService();
            _utilities = new Utilities.Utilities();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapExamEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Exam
            {
                Id = "guid",
                ExamSessionId = "guid",
                Location = "Somewhere",
                TimeOfStart = new DateTime(2023, 5, 28, 9, 0, 0),
                TimeOfEnd = new DateTime(2023, 5, 28, 11, 0, 0)
            };

            // Act
            var result = _examService.MapExamEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.ExamSessionId, result.ExamSessionId);
            Assert.AreEqual(entity.Location, result.Location);
            Assert.AreEqual(entity.TimeOfStart, result.TimeOfStart);
            Assert.AreEqual(entity.TimeOfEnd, result.TimeOfEnd);
        }

        [Test]
        public void MapingSemesterEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Semester
            {
                Id = "guid",
                Number = 2,
                UniversityId = "guid",
                Type = "Summer",
                DateOfStart = new DateTime(2023, 1, 1),
                DateOfEnd = new DateTime(2023, 5, 31)
            }; 

            // Act
            var result = _semesterService.MapSemesterEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Number, result.Number);
            Assert.AreEqual(entity.UniversityId, result.UniversityId);
            Assert.AreEqual(entity.Type, result.Type);
            Assert.AreEqual(entity.DateOfStart, result.DateOfStart);
            Assert.AreEqual(entity.DateOfEnd, result.DateOfEnd);
        }

        [Test]
        public void MapFacultyEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Faculty
            {
                Id = "guid",
                Name = "Faculty of Science",
                DeanId = "guid",
                ViceDeanId = "guid",
                UniversityId = "guid",
                Code = "FS"
            };
             
            // Act
            var result = _facultyService.MapFacultyEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Name, result.Name);
            Assert.AreEqual(entity.DeanId, result.DeanId);
            Assert.AreEqual(entity.ViceDeanId, result.ViceDeanId);
            Assert.AreEqual(entity.UniversityId, result.UniversityId);
            Assert.AreEqual(entity.Code, result.Code);
        }

        [Test]
        public void MapHolidayEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Holiday
            {
                Name = "New Year's Day",
                DateOfStart = new DateTime(2023, 1, 1),
                DateOfEnd = new DateTime(2023, 1, 1)
            };
             
            // Act
            var result = _holidayService.MapHolidayEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Name, result.Name);
            Assert.AreEqual(entity.DateOfStart, result.DateOfStart);
            Assert.AreEqual(entity.DateOfEnd, result.DateOfEnd);
        }

        [Test]
        public void MapSpecialityEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Speciality
            {
                Id = "guid",
                Name = "Computer Science",
                Degree = "Bachelor",
                Code = "CS",
                TutorId = "guid",
                Semesters = 8
            };
             
            // Act
            var result = _specialityService.MapSpeciallityEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Name, result.Name);
            Assert.AreEqual(entity.Degree, result.Degree);
            Assert.AreEqual(entity.Code, result.Code);
            Assert.AreEqual(entity.TutorId, result.TutorId);
            Assert.AreEqual(entity.Semesters, result.Semesters);
        }

        [Test]
        public void MapStaffEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Staff
            {
                Id = "guid",
                UserId = "guid",
                Role = "Dean"
            };
             
            // Act
            var result = _staffService.MapStaffEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.UserId, result.UserId);
            Assert.AreEqual(entity.Role, result.Role);
        }

        [Test]
        public void MapStudentCourseEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new StudentCourse
            {
                StudentId = "guid",
                Course = 1,
                SemesterId = "guid"
            };
             
            // Act
            var result = _studentCourseService.MapStudentCourseEntity(entity);

            // Assert 
            Assert.AreEqual(entity.StudentId, result.StudentId);
            Assert.AreEqual(entity.Course, result.Course);
            Assert.AreEqual(entity.SemesterId, result.SemesterId);
        }

        [Test]
        public void MapStudentEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Student
            {
                Id = "guid",
                UserId = "guid",
                SpecialityId = "guid",
                Citizenship = "USA",
                Identity = "123456789",
                DateOfStarting = new DateTime(2020, 9, 1),
                DateOfGraduate = new DateTime(2024, 6, 30),
                MunicipalityOfBirth = "Sample Municipality",
                AreaOfBirth = "Sample Area",
                DateOfBirth = new DateTime(2000, 1, 1),
                CountryOfBirth = "Sample Country",
                CityOfBirth = "Sample City",
                FacultyNumber = "A123456",
                FormOfEducation = "Full-time"
            };
             
            // Act
            var result = _studentService.MapStudentEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.UserId, result.UserId);
            Assert.AreEqual(entity.SpecialityId, result.SpecialityId);
            Assert.AreEqual(entity.Citizenship, result.Citizenship);
            Assert.AreEqual(entity.Identity, result.Identity);
            Assert.AreEqual(entity.DateOfStarting, result.DateOfStarting);
            Assert.AreEqual(entity.DateOfGraduate, result.DateOfGraduate);
            Assert.AreEqual(entity.MunicipalityOfBirth, result.MunicipalityOfBirth);
            Assert.AreEqual(entity.AreaOfBirth, result.AreaOfBirth);
            Assert.AreEqual(entity.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(entity.CountryOfBirth, result.CountryOfBirth);
            Assert.AreEqual(entity.CityOfBirth, result.CityOfBirth);
            Assert.AreEqual(entity.FacultyNumber, result.FacultyNumber);
            Assert.AreEqual(entity.FormOfEducation, result.FormOfEducation);
        }

        [Test]
        public void MapSubjectEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Subject
            {
                Id = "guid",
                TeacherId = "guid",
                SpecialityId = "guid",
                Name = "Mathematics",
                Type = "Core",
                Credits = 2,
                List = 0
            };
             
            // Act
            var result = _subjectService.MapSubjectEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.TeacherId, result.TeacherId);
            Assert.AreEqual(entity.SpecialityId, result.SpecialityId);
            Assert.AreEqual(entity.Name, result.Name);
            Assert.AreEqual(entity.Type, result.Type);
            Assert.AreEqual((decimal)entity.Credits, result.Credits);
            Assert.AreEqual(entity.List, result.List);
        }

        [Test]
        public void MapUniversityEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new University
            {
                Id = "guid",
                Name = "Sample University",
                RectorId = "guid",
                Address = "123 Main Street",
                Capacity = 5000
            };
             
            // Act
            var result = _universityService.MapUniversityEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Name, result.Name);
            Assert.AreEqual(entity.RectorId, result.RectorId);
            Assert.AreEqual(entity.Address, result.Address);
            Assert.AreEqual(entity.Capacity, result.Capacity);
        }

        [Test]
        public void MapUserEntity_WithValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new User
            {
                Id = "guid",
                Username = "john_doe",
                PasswordSalt = "somesalt",
                Password = "hashedpassword",
                FirstName = "John",
                MiddleName = "Smith",
                LastName = "Doe",
                DateOfRegistration = new DateTime(2020, 1, 1),
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                Address = "123 Main Street",
                Gender = "Male",
                Image = "profile.jpg",
                IsConfirmed = true
            };
             
            // Act
            var result = _userService.MapUserEntity(entity);

            // Assert 
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.Username, result.Username);
            Assert.AreEqual(entity.PasswordSalt, result.PasswordSalt);
            Assert.AreEqual(entity.Password, result.Password);
            Assert.AreEqual(entity.FirstName, result.FirstName);
            Assert.AreEqual(entity.MiddleName, result.MiddleName);
            Assert.AreEqual(entity.LastName, result.LastName);
            Assert.AreEqual(entity.DateOfRegistration, result.DateOfRegistration);
            Assert.AreEqual(entity.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(entity.Email, result.Email);
            Assert.AreEqual(entity.Address, result.Address);
            Assert.AreEqual(entity.Gender, result.Gender);
            Assert.AreEqual(entity.Image, result.Image);
            Assert.AreEqual(entity.FirstName + " " + entity.MiddleName + " " + entity.LastName, result.FullName);
            Assert.AreEqual(entity.IsConfirmed, result.IsConfirmed);
        }

        [Test]
        public void HashPasswordWithSalt_WithValidInputs_ReturnsHashedPassword()
        {
            // Arrange
            var password = "myPassword";
            var salt = "somesalt";
             
            // Act
            var result = _utilities.HashPasswordWithSalt(password, salt);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<byte[]>(result);

            // You can further validate the result if necessary, for example:
            var expectedBytes = Encoding.UTF8.GetBytes(password + salt);
            CollectionAssert.AreEqual(expectedBytes, result);
        }
    }
}
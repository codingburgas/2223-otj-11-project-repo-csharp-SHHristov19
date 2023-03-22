CREATE DATABASE Univers

GO

USE Univers

CREATE TABLE [Users] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [Username] NVARCHAR(30) NOT NULL,
  [Password] VARCHAR(100) NOT NULL,
  [PasswordSalt] VARCHAR(50) NULL,
  [FirstName] NVARCHAR(30) NOT NULL,
  [MiddleName] NVARCHAR(30) NOT NULL,
  [LastName] NVARCHAR(30) NOT NULL,
  [DateOfRegistration] DATETIME2 NOT NULL,
  [PhoneNumber] VARCHAR(16) NOT NULL,
  [Email] VARCHAR(50) NOT NULL,
  [Address] NVARCHAR(MAX) NOT NULL,
  [Gender] NVARCHAR(10) NOT NULL,
  [Image] VARCHAR(MAX) NULL,
  [IsActive] BIT DEFAULT 'TRUE' NOT NULL 
);

CREATE TABLE [Staff] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UserId] VARCHAR(50) FOREIGN KEY REFERENCES [Users]([Id]) NOT NULL,
  [Role] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Universities] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [RectorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NOT NULL,
  [Name] NVARCHAR(MAX) NULL,
  [Address] NVARCHAR(MAX) NULL,
  [Capacity] INT NULL
);

CREATE TABLE [Specialities] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [TutorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [Name] NVARCHAR(200) NOT NULL,
  [Degree] NVARCHAR(100) NULL,
  [Semesters] INT NULL,
  [Code] NVARCHAR(20) NULL
);

CREATE TABLE [Faculties] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UniversityId] VARCHAR(50) FOREIGN KEY REFERENCES [Universities]([Id]) NOT NULL,
  [DeanId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NOT NULL,
  [ViceDeanId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [Name] NVARCHAR(200) NULL,
  [Code] NVARCHAR(20) NULL
);

CREATE TABLE [FacultySpeciality] 
(
  [FacultyId] VARCHAR(50) FOREIGN KEY REFERENCES [Faculties]([Id]) NOT NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NOT NULL
);

CREATE TABLE [Subjects] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [TeacherId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NOT NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NOT NULL,
  [Name] NVARCHAR(150) NULL,
  [Type] NVARCHAR(100) NULL,
  [Credits] DECIMAL(2,1) NULL,
  [List] INT NULL
);

CREATE TABLE [Semesters] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UniversityId] VARCHAR(50) FOREIGN KEY REFERENCES [Universities]([Id]) NOT NULL,
  [Number] INT NULL,
  [Type] NVARCHAR(50) NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL
);

CREATE TABLE [SubjectSemester] 
(
  [SemesterId] VARCHAR(50) FOREIGN KEY REFERENCES [Semesters]([Id]) NOT NULL,
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NOT NULL
);

CREATE TABLE [ExamSessions] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL,
  [Type] NVARCHAR(100) NULL
);

CREATE TABLE [Students] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NOT NULL,
  [UserId] VARCHAR(50) FOREIGN KEY REFERENCES [Users]([Id]) NOT NULL,
  [Identity] VARCHAR(10) NULL,
  [Citizenship] NVARCHAR(60) NULL,
  [DacultyNumber] NVARCHAR(20) NULL,
  [DateOfStarting] DATETIME2 NULL,
  [DateOfGraduate] DATETIME2 NULL,
  [FormOfEducation] NVARCHAR(50) NULL,
  [DateOfBirth] DATETIME2 NULL,
  [CountryOfBirth] NVARCHAR(50) NULL,
  [MunicipalityOfBirth] NVARCHAR(50) NULL,
  [AreaOfBirth] NVARCHAR(50) NULL,
  [CityOfBirth] NVARCHAR(50) NULL,
  [FacultyNumber] NVARCHAR(50) NULL
);

CREATE TABLE [Components] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [Type] NVARCHAR(60) NOT NULL,
  [Activity] NVARCHAR(MAX) NULL,
  [DateOfStart] DATETIME2 NULL,
  [Duration] TIME NULL,
  [Location] NVARCHAR(50) NULL,
  [Note] NVARCHAR(MAX) NULL
);

CREATE TABLE [SubjectComponents] 
(
  [InstructorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NOT NULL,
  [ComponentId] VARCHAR(50) FOREIGN KEY REFERENCES [Components]([Id]) NOT NULL,
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NOT NULL
);

CREATE TABLE [StudentCourse] 
(
  [StudentId] VARCHAR(50) FOREIGN KEY REFERENCES [Students]([Id]) NOT NULL,
  [SemesterId] VARCHAR(50) FOREIGN KEY REFERENCES [Semesters]([Id]) NOT NULL,
  [Course] INT NULL
);

CREATE TABLE [Exams] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [ProctorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NOT NULL,
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NOT NULL,
  [ExamSessionId] VARCHAR(50) FOREIGN KEY REFERENCES [ExamSessions]([Id]) NOT NULL,
  [TimeOfStart] DATETIME2 NULL,
  [TimeOfEnd] DATETIME2 NULL,
  [Location] NVARCHAR(50) NULL
);

CREATE TABLE [Grades] 
(
  [StudentId] VARCHAR(50) FOREIGN KEY REFERENCES [Students]([Id]) NOT NULL,
  [ExamId] VARCHAR(50) FOREIGN KEY REFERENCES [Exams]([Id]) NOT NULL,
  [Grade] INT NULL
);

CREATE TABLE [Holidays] 
(
  [Name] NVARCHAR(100) NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL
);
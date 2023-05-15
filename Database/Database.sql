CREATE DATABASE Univers

GO

USE Univers

CREATE TABLE [Users] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [Username] NVARCHAR(30) NULL,
  [Password] VARCHAR(150) NULL,
  [PasswordSalt] VARCHAR(50) NULL,
  [FirstName] NVARCHAR(30) NULL,
  [MiddleName] NVARCHAR(30) NULL,
  [LastName] NVARCHAR(30) NULL,
  [DateOfRegistration] DATETIME2 NULL,
  [PhoneNumber] VARCHAR(16) NULL,
  [Email] VARCHAR(50) NULL,
  [Address] NVARCHAR(MAX) NULL,
  [Gender] NVARCHAR(10) NULL,
  [Image] VARCHAR(MAX) NULL,
  [IsActive] BIT DEFAULT 'TRUE' NULL 
);

CREATE TABLE [Staff] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UserId] VARCHAR(50) FOREIGN KEY REFERENCES [Users]([Id]) NULL,
  [Role] NVARCHAR(50) NULL
);

CREATE TABLE [Universities] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [RectorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [Name] NVARCHAR(MAX) NULL,
  [Address] NVARCHAR(MAX) NULL,
  [Capacity] INT NULL
);

CREATE TABLE [Specialities] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [TutorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [Name] NVARCHAR(200) NULL,
  [Degree] NVARCHAR(100) NULL,
  [Semesters] INT NULL,
  [Code] NVARCHAR(20) NULL
);

CREATE TABLE [Faculties] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UniversityId] VARCHAR(50) FOREIGN KEY REFERENCES [Universities]([Id]) NULL,
  [DeanId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [ViceDeanId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [Name] NVARCHAR(200) NULL,
  [Code] NVARCHAR(20) NULL
);

CREATE TABLE [FacultySpeciality] 
(
  [FacultyId] VARCHAR(50) FOREIGN KEY REFERENCES [Faculties]([Id]) NOT NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NOT NULL,
  PRIMARY KEY([FacultyId], [SpecialityId])
);

CREATE TABLE [Subjects] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [TeacherId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NULL,
  [Name] NVARCHAR(150) NULL,
  [Type] NVARCHAR(100) NULL,
  [Credits] DECIMAL(2,1) NULL,
  [List] INT NULL
);

CREATE TABLE [Semesters] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [UniversityId] VARCHAR(50) FOREIGN KEY REFERENCES [Universities]([Id]) NULL,
  [Number] INT NULL,
  [Type] NVARCHAR(50) NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL
);

CREATE TABLE [SubjectSemester] 
(
  [SemesterId] VARCHAR(50) FOREIGN KEY REFERENCES [Semesters]([Id]) NOT NULL,
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NOT NULL,
  PRIMARY KEY([SemesterId], [SubjectId])
);

CREATE TABLE [ExamSessions] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [SemesterId] VARCHAR(50) FOREIGN KEY REFERENCES [Semesters]([Id]) NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL,
  [Type] NVARCHAR(100) NULL
);

CREATE TABLE [Students] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [SpecialityId] VARCHAR(50) FOREIGN KEY REFERENCES [Specialities]([Id]) NULL,
  [UserId] VARCHAR(50) FOREIGN KEY REFERENCES [Users]([Id]) UNIQUE NULL,
  [Identity] VARCHAR(10) NULL,
  [Citizenship] NVARCHAR(60) NULL,
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
  [Type] NVARCHAR(60) NULL,
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
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NOT NULL,
  PRIMARY KEY([InstructorId], [ComponentId], [SubjectId])
);

CREATE TABLE [StudentCourse] 
(
  [StudentId] VARCHAR(50) FOREIGN KEY REFERENCES [Students]([Id]) NOT NULL,
  [SemesterId] VARCHAR(50) FOREIGN KEY REFERENCES [Semesters]([Id]) NOT NULL,
  [Course] INT NULL,
  PRIMARY KEY([StudentId], [SemesterId])
);

CREATE TABLE [Exams] 
(
  [Id] VARCHAR(50) PRIMARY KEY NOT NULL,
  [ProctorId] VARCHAR(50) FOREIGN KEY REFERENCES [Staff]([Id]) NULL,
  [SubjectId] VARCHAR(50) FOREIGN KEY REFERENCES [Subjects]([Id]) NULL,
  [ExamSessionId] VARCHAR(50) FOREIGN KEY REFERENCES [ExamSessions]([Id]) NULL,
  [TimeOfStart] DATETIME2 NULL,
  [TimeOfEnd] DATETIME2 NULL,
  [Location] NVARCHAR(50) NULL
);

CREATE TABLE [Grades] 
(
  [StudentId] VARCHAR(50) FOREIGN KEY REFERENCES [Students]([Id]) NOT NULL,
  [ExamId] VARCHAR(50) FOREIGN KEY REFERENCES [Exams]([Id]) NOT NULL,
  [Grade] INT NULL,
  PRIMARY KEY([StudentId], [ExamId])
);

CREATE TABLE [Holidays] 
(
  [Name] NVARCHAR(100) NULL,
  [DateOfStart] DATE NULL,
  [DateOfEnd] DATE NULL
);
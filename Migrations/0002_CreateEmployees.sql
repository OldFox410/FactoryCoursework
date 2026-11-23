CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    LastName NVARCHAR(50) NOT NULL,
    BadgeNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(MAX),
    Phone NVARCHAR(20),
    DateOfBirth DATE,
    DepartmentId INT NOT NULL,
    CONSTRAINT FK_Employees_Department FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);
CREATE TABLE Payments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId INT NOT NULL,
    SalaryAmount DECIMAL(18, 2) NOT NULL,
    PaymentMonth DATE NOT NULL,
    Bonus DECIMAL(18, 2) DEFAULT 0,
    Fine DECIMAL(18, 2) DEFAULT 0,
    CONSTRAINT FK_Payments_Employee FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
);

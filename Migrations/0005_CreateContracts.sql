CREATE TABLE Contracts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrganizationId INT NOT NULL,
    EmployeeId INT NOT NULL,
    ContractDate DATE,
    CONSTRAINT FK_Contracts_Organization FOREIGN KEY (OrganizationId) REFERENCES Organizations(Id),
    CONSTRAINT FK_Contracts_Employee FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
);
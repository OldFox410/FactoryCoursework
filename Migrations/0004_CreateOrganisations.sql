CREATE TABLE Organizations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Address NVARCHAR(MAX),
    ContactNumber NVARCHAR(50)
);
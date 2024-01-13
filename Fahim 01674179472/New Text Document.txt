CREATE DATABASE EmployeeDB
go;

use EmployeeDB


CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    EmployeeName NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18, 2),
    EmployeeStatus NVARCHAR(20),
    IsAvailable BIT,
    
);

CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employee(EmployeeId)
);

-- Insert sample data into Employee table
INSERT INTO Employee (EmployeeId, EmployeeName, Salary, EmployeeStatus, IsAvailable) VALUES
    (1, 'Fahim', 50000.00, 'Active', 1),
    (2, 'Jamal', 60000.00, 'Active', 1),
    (3, 'Babul', 55000.00, 'Inactive', 0),
    (4, 'Kamal', 52000.00, 'Active', 1);

INSERT INTO Department (DepartmentId, DepartmentName, EmployeeId) VALUES
    (1, 'HumanREesource Department',1),
    (2, 'Information Department',2),
    (3, 'CustomerSupport Department',3);

	SELECT 
    e.EmployeeId,
    e.EmployeeName,
    e.Salary,
    e.EmployeeStatus,
    e.IsAvailable,
    d.DepartmentId,
    d.DepartmentName
   
FROM 
    Employee e
JOIN 
    Department d ON e.EmployeeId = d.EmployeeId;
GO

	SELECT 
    d.DepartmentId,
    d.DepartmentName,
    COUNT(e.EmployeeId) AS EmployeeCount
FROM 
    Department d
LEFT JOIN 
    Employee e ON d.DepartmentId = e.EmployeeId
GROUP BY 
    d.DepartmentId, d.DepartmentName;
GO
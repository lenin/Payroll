CREATE DATABASE Payroll;
GO
USE Payroll

DROP TABLE PaycheckAddress;
DROP TABLE DirectDepositAccount;
DROP TABLE Employee;
GO

CREATE TABLE Employee (
	EmpId				int primary key,
	Name				varchar(50),
	Address				varchar(50),
	ScheduleType			varchar(50),
	PaymentMethodType		varchar(50),
	PaymentClassificationType	varchar(50)
)
GO

CREATE TABLE DirectDepositAccount (
	Bank	varchar(50) NOT NULL,
	Account varchar(50) NOT NULL,
	EmpId	int REFERENCES Employee
)
GO

CREATE TABLE PaycheckAddress (
	Address	varchar(50) NOT NULL,
	EmpId	int REFERENCES Employee
)
GO

CREATE TABLE SalariedClassification (
	Salary	decimal(10, 2) NOT NULL,
	EmpId	int REFERENCES Employee
)
GO


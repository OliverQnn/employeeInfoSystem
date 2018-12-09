--******采用驼峰命名法*******--
--即所有命名的第一个单词的第一个字母小写后面的每个单词的首字母大写--

--*******创建公司员工管理系统数据库*******--
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'employeeInfoSystem')  
DROP DATABASE employeeInfoSystem
GO
CREATE DATABASE employeeInfoSystem
GO

--*******使用数据库*******--
USE employeeInfoSystem
GO

--*******创建职工信息表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'employeeInfo')
DROP TABLE employeeInfo
GO
CREATE TABLE employeeInfo
(
		employeeId INT PRIMARY KEY IDENTITY(1, 1),	--职工ID，自增长1
		employeeName NVARCHAR(30)NOT NULL,	--职工姓名
		employeeSex INT NOT NULL,	--职工性别	0女1男
		employeePhone INT NOT NULL,	--职工电话
		employeeBir DATETIME NULL,	--职工生日
		employeeIdentity NVARCHAR(18) NOT NULL UNIQUE,	--职工身份证编号，唯一
)
GO	

--*******创建公司部门表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'department')
DROP TABLE department
GO
CREATE TABLE department
(
		departmentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,	--公司部门编号,自增长
		departmentName NVARCHAR(10),	--公司部门名称
)
GO

--*******创建公司职务表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'companyPosition')
DROP TABLE companyPosition
GO
CREATE TABLE companyPosition
(
		positionId INT PRIMARY KEY IDENTITY(1,1),	--职位编号
		departmentId INT NOT NULL,	--所属部门Id，可以根据Id查找名字
		positionName NVARCHAR(30) NOT NULL,	--职位名称
		positionSalary FLOAT NULL,	--薪酬
		
)
GO

--*******创建公司就职信息表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'employeePerformance')
DROP TABLE employeePerformance
GO
CREATE TABLE employeePerformance
(
		employeeId INT PRIMARY KEY,	--职工编号
		positionName NVARCHAR(30) NOT NULL,	--职位名称
		performance FLOAT NULL,	--业绩（按月表示。例：10000，表示一个月为公司创造的价值是10000元）
) 
GO



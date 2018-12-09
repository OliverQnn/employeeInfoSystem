--******�����շ�������*******--
--�����������ĵ�һ�����ʵĵ�һ����ĸСд�����ÿ�����ʵ�����ĸ��д--

--*******������˾Ա������ϵͳ���ݿ�*******--
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'employeeInfoSystem')  
DROP DATABASE employeeInfoSystem
GO
CREATE DATABASE employeeInfoSystem
GO

--*******ʹ�����ݿ�*******--
USE employeeInfoSystem
GO

--*******����ְ����Ϣ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'employeeInfo')
DROP TABLE employeeInfo
GO
CREATE TABLE employeeInfo
(
		employeeId INT PRIMARY KEY IDENTITY(1, 1),	--ְ��ID��������1
		employeeName NVARCHAR(30)NOT NULL,	--ְ������
		employeeSex INT NOT NULL,	--ְ���Ա�	0Ů1��
		employeePhone INT NOT NULL,	--ְ���绰
		employeeBir DATETIME NULL,	--ְ������
		employeeIdentity NVARCHAR(18) NOT NULL UNIQUE,	--ְ�����֤��ţ�Ψһ
)
GO	

--*******������˾���ű�*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'department')
DROP TABLE department
GO
CREATE TABLE department
(
		departmentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,	--��˾���ű��,������
		departmentName NVARCHAR(10),	--��˾��������
)
GO

--*******������˾ְ���*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'companyPosition')
DROP TABLE companyPosition
GO
CREATE TABLE companyPosition
(
		positionId INT PRIMARY KEY IDENTITY(1,1),	--ְλ���
		departmentId INT NOT NULL,	--��������Id�����Ը���Id��������
		positionName NVARCHAR(30) NOT NULL,	--ְλ����
		positionSalary FLOAT NULL,	--н��
		
)
GO

--*******������˾��ְ��Ϣ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'employeePerformance')
DROP TABLE employeePerformance
GO
CREATE TABLE employeePerformance
(
		employeeId INT PRIMARY KEY,	--ְ�����
		positionName NVARCHAR(30) NOT NULL,	--ְλ����
		performance FLOAT NULL,	--ҵ�������±�ʾ������10000����ʾһ����Ϊ��˾����ļ�ֵ��10000Ԫ��
) 
GO



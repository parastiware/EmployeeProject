USE[Employee]
GO
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE EmployeeManagement @action varchar(25)=NULL,
@employeeName varchar(255)=NULL,
@dob varchar(255)=NULL,
@salary varchar(255)=NULL,
@gender varchar(12)=NULL,
@emp_id varchar(10)=NULL,
@Q_id varchar(10)=NULL,
@marks int=NULL
AS
BEGIN
 SET NOCOUNT ON;
 DECLARE @desc varchar(max)=null;
 BEGIN TRY
	BEGIN TRANSACTION  EmployeeManagement
		
		--read all employees
		IF @action='list'
		BEGIN
			 SELECT * FROM Employee;
		END

		--create employee
		IF @action='create'
		BEGIN
			 INSERT INTO Employee(Emp_name,DOB,Gender,Salary,Entry_by,Entry_date)
			  VALUES(@employeeName,@dob,@gender,@salary,SESSION_USER,GETDATE())
			  SELECT 0 Code,'Employee created successfully!!' message,SCOPE_IDENTITY() Extra1;
		END

		--update employee
		IF @action='update'
		BEGIN
			 UPDATE Employee SET Emp_name = isnull(@employeeName,Emp_name),
									   DOB = isnull(@dob,DOB),
									   Gender= isnull(@gender,Gender),
									   Salary=ISNULL(@salary,Salary)
							WHERE Employee_id=@emp_id
			  SELECT 0 Code,'Employee updated successfully!!' message;
		END

		--delete employee
		IF @action='delete'
		BEGIN
			 DELETE FROM emp_qualification WHERE Employee_id=@emp_id
			 DELETE FROM Employee WHERE Employee_id=@emp_id
			 SELECT 0 Code,'Employee deleted successfully!!' message;
		END

		--- section qalification 

		--- select employee qualification
		IF @action='qualification-get'
		BEGIN
			 SELECT Employee_id
			 ,Q_id
			 ,(Select Q_name from Qualificationlist ql where ql.Q_id= eq.Q_id) as Q_name
			 ,Marks FROM emp_qualification eq WHERE Employee_id=@emp_id
			
		END
		--- ADD/UPDATE employee qualification
		IF @action='qualification-manage'
		BEGIN
			 IF Exists(SELECT 'X' from emp_qualification  WHERE Employee_id=@emp_id and Q_id=@Q_id )
			  
				 BEGIN
					Update emp_qualification SET marks=@marks WHERE Employee_id=@emp_id and Q_id=@Q_id;
					SELECT 0 Code,'qualification updated successfully!!' message;
				 END
			 ELSE
				 BEGIN
					 INSERT INTO emp_qualification (Employee_id,Q_id,Marks) VALUES (@emp_id,@Q_id,@marks)
					 SELECT 0 Code,'qualification added successfully!!' message;
				END

			 
		END
		--delete qualification
		IF @action='qualification-delete'
		BEGIN
			 DELETE FROM emp_qualification WHERE Employee_id=@emp_id and Q_id=@Q_id;
			 SELECT 0 Code,'qualification removed successfully!!' message;
		END
		

		---  employee qualification dropdown
		IF @action='qualification-dropdown'
		BEGIN
			 SELECT * FROM QualificationList
			
		END

	COMMIT TRANSACTION  EmployeeManagement
 END TRY
 BEGIN CATCH
 
				IF @@trancount > 0
					ROLLBACK TRANSACTION EmployeeManagement

				SET @desc = 'sql error found:(' + cast(error_message() AS VARCHAR) + ')' + ' at ' + cast(error_line() AS VARCHAR)
				SELECT 1 Code ,@desc message;
 END CATCH 

END
GO

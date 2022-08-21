
CREATE TABLE [Employee]
(
   Employee_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
   Emp_name varchar(255) NOT NULL ,
   DOB varchar(255) NOT NULL ,
   Gender  VARCHAR(12) NOT NULL CHECK (Gender IN('Male', 'Female', 'third gender')),
   Salary varchar(50),
   Entry_by varchar(255) NOT NULL ,
   Entry_date varchar(255) NOT NULL ,

);

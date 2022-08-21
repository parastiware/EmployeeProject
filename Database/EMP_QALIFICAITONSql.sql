
CREATE TABLE [EMP_QUALIFICAITON]
(
   Employee_id INT NOT NULL,
   Q_id INT NOT NULL,
   Marks INT NOT NULL,
   CONSTRAINT FK_EmployeeQualificaiton FOREIGN KEY (Employee_id)
   REFERENCES Employee(Employee_id),
   CONSTRAINT FK_QualificaitonList FOREIGN KEY (Q_id)
   REFERENCES QualificationList(Q_id)
);

CREATE TABLE [QualificationList]
(
   Q_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
   Q_name varchar(255) NOT NULL ,
);


INSERT INTO [QualificationList] (Q_name) VALUES( 'SLC');
INSERT INTO [QualificationList] (Q_name) VALUES( 'Intermidate');
INSERT INTO [QualificationList] (Q_name) VALUES( 'BE');
INSERT INTO [QualificationList] (Q_name) VALUES( 'ME');
INSERT INTO [QualificationList] (Q_name) VALUES( 'PHD');

create database GetFund
Drop database GetFund

create table GUser
(
ID int IDENTITY(1,1) PRIMARY KEY,
Email varchar(50) NULL,
Password varchar(50) NOT NULL CHECK (LEN(Password) >= 6),
IsValid Bit default 'FALSE',
);


INSERT INTO GUser(Email,Password,IsValid)
VALUES ('nafiahmed318@gmail.com','123456','TRUE');

Select * from GUser
Drop table GUser
create table Profile
(
ID int foreign key REFERENCES GUser (ID),
Name varchar(50) NOT NUll,
Address varchar(50) default 'Dhaka',
NID int  NULL
);


INSERT INTO Profile(ID,Name,Address,NID)
VALUES (1,'Nafi Ahmed','Mirpur somewhere',897564);
Drop table Profile
create table Project
(
PId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
Title varchar(50),
Info varchar(50) ,
VideoLink varchar(250) ,
Type varchar(50) ,
Target varchar(50),
MoneyRaised float default 0.00
);
Select * From Project

INSERT INTO Project(ID,Title,Info,MoneyRaised)
VALUES (1,'First Project','The project info from database is here',10000.00);

Drop table Project
create table Comments
(
ID int foreign key REFERENCES GUser (ID),
PId int foreign key REFERENCES Project (PId),
Comment varchar(50) NOT NULL,
Moment DATETIME  NOT NUll,
Amount float Not Null
);
Drop table Comments
create table Donation
(
DonationId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
PId int foreign key REFERENCES Project (PId),
DonateDes varchar(50) NOT NULL,
DonateTime DATETIME NOT NUll,
transaction_id int NOT NUll,
Amount float Not Null
);
Drop table Donation

DELETE FROM GUser WHERE ID=4;

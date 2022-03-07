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
VALUES ('B@gmail.com','123456','TRUE');

Select * from Profile

create table Profile
(
ProfileId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
Name varchar(50) NOT NUll,
ProfilePicture varchar(255) NUll,
Address varchar(50) default 'Dhaka',
NID int  NULL
);
Drop Table Profile

INSERT INTO Profile(ID,Name,Address,NID)
VALUES (1,'Swap','Mirpur, Dhaka',12456498);

create table Project
(
PId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
Title varchar(50) NOT NUll,
Info varchar(50) NOT NULL,
VideoLink varchar(250) NOT NULL,
Type varchar(50) NOT NULL,
Target varchar(50),
ProjectImage1 varchar(255),
Likes int,
MoneyRaised float default 0.00
);
Select * From Donation
Select * From GUser

create table Comments
(
CommentId int IDENTITY(1,1) PRIMARY KEY,
CName varchar(50),
PId int foreign key REFERENCES Project (PId),
Comment varchar(50) NOT NULL,
);
INSERT INTO Comments (ID,PId, Comment)
VALUES (1,1, 'Nice idea');
Drop table Comments

create table Donation
(
DonationId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
PId int foreign key REFERENCES Project (PId),
DonateDes varchar(50) NOT NULL,
DonateTime varchar(50) NOT NUll,
transaction_id varchar(50) NOT NUll,
Amount int Not Null
);
INSERT INTO Donation (ID,PId, DonateTime,DonateDes,transaction_id,Amount)
VALUES (1,1, '25 March 2022', 'Something', '2455AFGBG5265DG',2000);
 Select * from Donation


DELETE FROM GUser WHERE ID=4;

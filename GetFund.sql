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

Select * from GUser

create table Profile
(
ID int foreign key REFERENCES GUser (ID),
Name varchar(50) NOT NUll,
Address varchar(50) default 'Dhaka',
NID int  NULL
);

INSERT INTO Profile(ID,Name,Address,NID)
VALUES (3,'Swap','Dhaka',15479684);

create table Project
(
PId int IDENTITY(1,1) PRIMARY KEY,
ID int foreign key REFERENCES GUser (ID),
Title varchar(50) NOT NUll,
Info varchar(50) NOT NULL,
VideoLink varchar(250) NOT NULL,
Type varchar(50) NOT NULL,
Target varchar(50),
MoneyRaised float default 0.00
);
Select * From Project

create table Comments
(
ID int foreign key REFERENCES GUser (ID),
PId int foreign key REFERENCES Project (PId),
Comment varchar(50) NOT NULL,
Moment DATETIME  NOT NUll,
Amount float Not Null
);

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


DELETE FROM GUser WHERE ID=4;

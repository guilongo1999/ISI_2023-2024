Use Database_API;
GO

CREATE TABLE UserInfo
(
ID int identity primary key,
Username varchar(50),
passwordHash varbinary(1024),
passwordSalt varbinary(1024)
);
GO

Create Table EmailDto
(
ID int identity primary key,
"To" varchar(200),
"subject" varchar(200),
"Body" varchar(1000)
);

Create Table UtilizadorAuth
(
ID int identity  primary key,
Username varchar(200),
"Password" varchar(200)
);
GO

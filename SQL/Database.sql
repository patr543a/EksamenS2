USE master;
GO
DROP DATABASE IF EXISTS CampingDB;
CREATE DATABASE CampingDB;
USE CampingDB;
GO

create table Pitches(
	PitchId int primary key identity(0,1) not null,
	PitchName nvarchar(10) not null,
);

create table Customers(
	CustomerId int primary key identity(0,1) not null,
	FullName nvarchar(100) not null,
	EmailAddress nvarchar(100) not null,
	PhoneNumber nvarchar(15),
);

create table Bookings(
	BookingId int primary key identity(0,1) not null,
	StartDate datetime not null,
	EndDate datetime not null,
	Adults int not null,
	Children int not null,
	PitchId int foreign key references Pitches(PitchId) not null,
	CustomerId int foreign key references Customers(CustomerId) not null,
);

insert into Pitches(PitchName) values ('10A');
insert into Pitches(PitchName) values ('10B');
insert into Pitches(PitchName) values ('11A');
insert into Pitches(PitchName) values ('11B');
insert into Pitches(PitchName) values ('12A');
insert into Pitches(PitchName) values ('12B');
insert into Pitches(PitchName) values ('14A');
insert into Pitches(PitchName) values ('14B');
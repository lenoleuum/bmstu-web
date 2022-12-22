\c mbti_db

create schema MBTI;

create table MBTI.Users
(
	UserUK int primary key,
	Login varchar(100) not null ,
	Password varchar(50) not null ,
	Nickname varchar(100),
	Email varchar(255),
	Telagram varchar(255),
	TypeUK int,
	DateOfBirth date
);

create table MBTI.Types
(
	TypeUK int primary key,
	TypeName varchar(255) not null ,
	TypeDescription text
);

create table MBTI.Characters
(	
	CharacterUK int primary key,
	CharacterName varchar(255) not null,
	TypeUK int not null,
	Category varchar(100)
);

alter table MBTI.Characters add constraint Characters_TypeUk_FK foreign key (TypeUK) references MBTI.Types(TypeUK);

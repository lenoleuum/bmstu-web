\c mbti_db

create table users
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

create table types
(
	TypeUK int primary key,
	TypeName varchar(255) not null ,
	TypeDescription text
);

create table characters
(	
	CharacterUK int primary key,
	CharacterName varchar(255) not null,
	TypeUK int not null,
	Category varchar(100)
);

alter table characters add constraint Characters_TypeUk_FK foreign key (TypeUK) references types(TypeUK);

create database Test
go

use [Test]
go

create table dbo.[Role]
(
[ID_Role] int not null identity(1,1) primary key,
[Role_Name] varchar(50) not null,
);
go

insert into dbo.[Role] ([Role_Name])
values
('Админ'),
('Клиент')
go

select * from dbo.[Role]

create table dbo.[User]
(
[ID_User] int not null identity(1,1) primary key,
[Login] varchar(50) not null unique,
[Password] varchar(50) not null,
[First_Name] varchar(50) not null,
[Second_Name] varchar(50) not null,
[Middle_Name] varchar(50) null default ('-'),
[Role_ID] int not null,
constraint [FK_Role_User] foreign key ([Role_ID]) references [dbo].[Role] ([ID_Role])
);
go

insert into dbo.[User] ([Login], [Password], [First_Name], [Second_Name], [Middle_Name], [Role_ID])
values
('User1', 12345, 'Алексеев', 'Михаил', 'Сергеевич', 1)
go

select * from dbo.[User]
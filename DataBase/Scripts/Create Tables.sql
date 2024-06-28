create table SiteUser(
	Id int not null PRIMARY KEY IDENTITY(1,1),
	Email nvarchar(255) NOT NULL,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null
);
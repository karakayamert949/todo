CREATE TABLE MK_Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(100) NOT NULL
);


INSERT INTO MK_Roles (RoleName) VALUES ('Admin'), ('User');


CREATE TABLE MK_Users (
    UserId INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(80) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(80) NOT NULL,
    RoleId INT FOREIGN KEY REFERENCES MK_Roles(RoleId)
);

/*
INSERT INTO MK_Users (FirstName, LastName, Email, PasswordHash, RoleId) 
VALUES ('Admin', 'AdminLastName', 'admin@example.com', HASHBYTES('SHA2_256', 'AdminPassword'), 1);
*/

CREATE TABLE MK_UserConnectionLogs (
    LogId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES MK_Users(UserId),
    LoginTime DATETIME NOT NULL,
    LogoutTime DATETIME NULL
);

CREATE TABLE MK_Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(50) NOT NULL
);

/*
INSERT INTO MK_Categories(CategoryName) VALUES
(N'Müzik'),
(N'Tiyatro');
*/

CREATE TABLE MK_Cities (
    CityId INT PRIMARY KEY IDENTITY,
    CityName NVARCHAR(50) NOT NULL
);

/*
INSERT INTO MK_Cities (CityName) VALUES 
(N'Adana'),
(N'Adýyaman'),
(N'Afyonkarahisar'),
(N'Aðrý'),
(N'Amasya'),
(N'Ankara'),
(N'Antalya'),
(N'Artvin'),
(N'Aydýn'),
(N'Balýkesir'),
(N'Bilecik'),
(N'Bingöl'),
(N'Bitlis'),
(N'Bolu'),
(N'Burdur'),
(N'Bursa'),
(N'Çanakkale'),
(N'Çankýrý'),
(N'Çorum'),
(N'Denizli'),
(N'Diyarbakýr'),
(N'Edirne'),
(N'Elazýð'),
(N'Erzincan'),
(N'Erzurum'),
(N'Eskiþehir'),
(N'Gaziantep'),
(N'Giresun'),
(N'Gümüþhane'),
(N'Hakkari'),
(N'Hatay'),
(N'Isparta'),
(N'Mersin'),
(N'Ýstanbul'),
(N'Ýzmir'),
(N'Kars'),
(N'Kastamonu'),
(N'Kayseri'),
(N'Kýrklareli'),
(N'Kýrþehir'),
(N'Kocaeli'),
(N'Konya'),
(N'Kütahya'),
(N'Malatya'),
(N'Manisa'),
(N'Kahramanmaraþ'),
(N'Mardin'),
(N'Muðla'),
(N'Muþ'),
(N'Nevþehir'),
(N'Niðde'),
(N'Ordu'),
(N'Rize'),
(N'Sakarya'),
(N'Samsun'),
(N'Siirt'),
(N'Sinop'),
(N'Sivas'),
(N'Tekirdað'),
(N'Tokat'),
(N'Trabzon'),
(N'Tunceli'),
(N'Þanlýurfa'),
(N'Uþak'),
(N'Van'),
(N'Yozgat'),
(N'Zonguldak'),
(N'Aksaray'),
(N'Bayburt'),
(N'Karaman'),
(N'Kýrýkkale'),
(N'Batman'),
(N'Þýrnak'),
(N'Bartýn'),
(N'Ardahan'),
(N'Iðdýr'),
(N'Yalova'),
(N'Karabük'),
(N'Kilis'),
(N'Osmaniye'),
(N'Düzce');
*/

CREATE TABLE MK_Activities (
    ActivityId INT PRIMARY KEY IDENTITY,
    CategoryId INT FOREIGN KEY REFERENCES MK_Categories(CategoryId),
    CityId INT FOREIGN KEY REFERENCES MK_Cities(CityId),
	Title NVARCHAR(255) NOT NULL,
	ImageSource NVARCHAR(255) NOT NULL,
	Description TEXT NOT NULL,
	AnnounceDate DATETIME NOT NULL,
	ParticipationDate DATETIME NOT NULL,
	ActivityDate DATETIME NOT NULL,
	CreatedAtDate DATETIME NOT NULL,
	WinnerCount INT NOT NULL
);

CREATE TABLE MK_Participations (
    ParticipationId INT PRIMARY KEY IDENTITY,
	UserId INT FOREIGN KEY REFERENCES MK_Users(UserId),
	ActivityId INT FOREIGN KEY REFERENCES MK_Activities(ActivityId)
);

CREATE TABLE MK_Winners (
    WinnerId INT PRIMARY KEY IDENTITY,
	UserId INT FOREIGN KEY REFERENCES MK_Users(UserId),
	ActivityId INT FOREIGN KEY REFERENCES MK_Activities(ActivityId)
);
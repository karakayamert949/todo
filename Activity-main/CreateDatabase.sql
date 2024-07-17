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
(N'M�zik'),
(N'Tiyatro');
*/

CREATE TABLE MK_Cities (
    CityId INT PRIMARY KEY IDENTITY,
    CityName NVARCHAR(50) NOT NULL
);

/*
INSERT INTO MK_Cities (CityName) VALUES 
(N'Adana'),
(N'Ad�yaman'),
(N'Afyonkarahisar'),
(N'A�r�'),
(N'Amasya'),
(N'Ankara'),
(N'Antalya'),
(N'Artvin'),
(N'Ayd�n'),
(N'Bal�kesir'),
(N'Bilecik'),
(N'Bing�l'),
(N'Bitlis'),
(N'Bolu'),
(N'Burdur'),
(N'Bursa'),
(N'�anakkale'),
(N'�ank�r�'),
(N'�orum'),
(N'Denizli'),
(N'Diyarbak�r'),
(N'Edirne'),
(N'Elaz��'),
(N'Erzincan'),
(N'Erzurum'),
(N'Eski�ehir'),
(N'Gaziantep'),
(N'Giresun'),
(N'G�m��hane'),
(N'Hakkari'),
(N'Hatay'),
(N'Isparta'),
(N'Mersin'),
(N'�stanbul'),
(N'�zmir'),
(N'Kars'),
(N'Kastamonu'),
(N'Kayseri'),
(N'K�rklareli'),
(N'K�r�ehir'),
(N'Kocaeli'),
(N'Konya'),
(N'K�tahya'),
(N'Malatya'),
(N'Manisa'),
(N'Kahramanmara�'),
(N'Mardin'),
(N'Mu�la'),
(N'Mu�'),
(N'Nev�ehir'),
(N'Ni�de'),
(N'Ordu'),
(N'Rize'),
(N'Sakarya'),
(N'Samsun'),
(N'Siirt'),
(N'Sinop'),
(N'Sivas'),
(N'Tekirda�'),
(N'Tokat'),
(N'Trabzon'),
(N'Tunceli'),
(N'�anl�urfa'),
(N'U�ak'),
(N'Van'),
(N'Yozgat'),
(N'Zonguldak'),
(N'Aksaray'),
(N'Bayburt'),
(N'Karaman'),
(N'K�r�kkale'),
(N'Batman'),
(N'��rnak'),
(N'Bart�n'),
(N'Ardahan'),
(N'I�d�r'),
(N'Yalova'),
(N'Karab�k'),
(N'Kilis'),
(N'Osmaniye'),
(N'D�zce');
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
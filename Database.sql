--CREATE DATABASE pesage;
--use pesage;
CREATE TABLE Client(
	id int IDENTITY(1,1) PRIMARY KEY,
	libelle VARCHAR(50)
)

CREATE TABLE C_Service(
	id int IDENTITY(1,1) PRIMARY KEY,
	libelle VARCHAR(50)
)

CREATE TABLE Operateur (
	id INT IDENTITY(1,1) PRIMARY KEY,
	libelle varchar(50)
)

CREATE TABLE Conteneur (
	id INT IDENTITY(1,1) PRIMARY KEY,
	libelle varchar(50)
)

CREATE TABLE Residu (
	id INT IDENTITY(1,1) PRIMARY KEY,
	libelle varchar(50)
)

CREATE TABLE client_service (
	id INT IDENTITY(1,1) PRIMARY KEY,
	client_id int foreign key references Client(id),
	service_id int foreign key references C_Service(id),
)

CREATE TABLE Utilisateur (
	id INT IDENTITY(1,1) PRIMARY KEY,
	nom varchar(50),
	mdp varchar(100)
)

CREATE TABLE Etiquette(
	id int IDENTITY(1,1) PRIMARY KEY,
	num_serie INT,
	e_date DATETIME default CURRENT_TIMESTAMP,
	poid float(10),
	client_id int foreign key references Client(id),
	service_id int foreign key references C_Service(id),
	conteneur_id int foreign key references Conteneur(id),
	residu_id int foreign key references Residu(id),
	operateur_id int foreign key references Operateur(id),
)
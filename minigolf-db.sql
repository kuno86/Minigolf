CREATE database IF NOT EXISTS Minigolf DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE Minigolf;

Drop table if exists Track;
Create Table Track(
	id int Primary Key not null, 
	title tinytext not null, 
	description tinytext, 
	data blob not null, 
	author_id int not null
	);
	
Drop table if exists Player;
Create Table Player(
	id int Primary Key not null AUTO_INCREMENT, 
	userName tinytext not null,
	passwordHash tinytext not null,
	email tinytext not null, 
	registerDate datetime not null,
	currentIP tinytext
	);
	
Drop table if exists Course;
Create Table Course(
	id int Primary Key not null, 
	name tinytext not null, 
	active tinyint not null
	);
	
Drop table if exists CourseTrack;
Create Table CourseTrack(
	id int Primary Key not null, 
	courseId int not null, 
	trackId int not null
	);
	
Drop table if exists PlayerWaitingGame;
Create Table PlayerWaitingGame(
	id int Primary Key not null, 
	playerId int not null, 
	waitingGameId int not null
	);
	
Drop table if exists PlayerTrackScore;
Create Table PlayerTrackScore(
	id int Primary Key not null, 
	playerId int not null, 
	trackId int not null,
	scoreValue tinyint
	);
	
Drop table if exists WaitingGame;
Create Table WaitingGame(
	id int Primary Key not null, 
	maxPlayers tinyint not null,
	password tinytext not null,
	courseTrackId int not null,
	maxHitsPerTrackId int not null,
	maxTimePerHitId int not null,
	waterTouchResetsBall tinyint not null,
	ballBallCollision tinyint not null
	);

Drop table if exists MaxHitsPerTrack;
Create Table MaxHitsPerTrack(
	id int Primary Key not null, 
	maxHits tinyint not null
	);

Drop table if exists MaxTimePerHit;
Create Table MaxTimePerHit(
	id int Primary Key not null, 
	maxTimeSec mediumint not null
	);
create database northpole;

-- create gifts table
create table gifts
(
	giftId int not null primary key auto_increment,
    giftName varchar(100)
);

-- create table for letters to santa
-- note that the giftID is associated with gifts table below
-- isGood is for santa to update status of person
create table letters 
(
	letterId int not null primary key auto_increment,
    `name` varchar(40),
    age int,
    city varchar(40),
    country varchar(40),
    note varchar(200),
    isGood bit,
    giftId int,
    foreign key (giftId) references gifts(giftId)
);

-- create employees table
create table employees 
(
	id int not null primary key auto_increment,
    fullName varchar(40),
    title varchar(40),
    userName varchar(40),
    `password` varchar(40)
);

-- inserting santa into table 
-- since he will have admin access
insert into employees
values (0, 'Santa Clause', 'Santa', 'santaclause', 'M#rryChr!stm@s');

-- setting up a tasks table for the elves/santa to work from
-- gift ID is foreign key for gift (one to many)
-- letterID is unique foreign key referencing a letter (one to one)
create table tasks 
(
	 id int not null auto_increment primary key,
     `name` varchar(30),
     `description` varchar(100),
     currentStatus varchar(50),
     giftId int,
     letterId int,
     foreign key (giftId) references gifts(giftId),
     unique (letterId),
     foreign key (letterId) references letters(letterId)
);

-- since employee-task is MtM relationship setting up
-- the employee task table
create table employeestasks 
(
	id int not null primary key auto_increment,
    employeeId int,
    taskId int,
    foreign key (employeeId) references employees(id),
    foreign key  (taskId) references tasks(id)
);

-- want to prepopulate a list of gifts for people to choose from
insert into gifts 
values(0, 'Lump of Coal'),
(0, 'Stuffed Animal'),
(0, 'Model Train'),
(0, 'Puzzle'),
(0, 'Book'),
(0, 'Doll'),
(0, 'Legos'),
(0, 'Action Figure');

-- select * from gifts;

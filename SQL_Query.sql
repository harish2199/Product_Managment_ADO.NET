create database product_managment_App
use product_managment_App
create table Product_Managment
(
	ID int identity primary key,
	Name varchar(20),
	Brand varchar(20),
    Quantity int,
	Price decimal(10,2)
)

--drop table Product_Managment
select * from product_Managment
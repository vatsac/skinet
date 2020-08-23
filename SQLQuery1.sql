Create Table Product(
Id int primary key identity,
Name Varchar(max)
)
insert into Product values('Product one'),('Product two'),('Product three')
Delete from Product
ALTER TABLE Product
    ADD 
		Description Varchar(max),
		Price decimal not null,
		PictureUrl varchar(max),
		ProductTypeId int not null
		Constraint FK_ProductTypeId Foreign Key(ProductTypeId)
        references ProductType(Id),
		ProductBrandId int not null
		Constraint Fk_ProductBrandId Foreign key(ProductBrandId)
		references ProductBrand(Id)


Create Table ProductType(
Id int primary key identity,
Name Varchar(max)
)

Create Table ProductBrand(
Id int primary key identity,
Name Varchar(max)
)

INSERT INTO ProductBrand(Name) VALUES ('Angular');
INSERT INTO ProductBrand(Name) VALUES ('NetCore');
INSERT INTO ProductBrand(Name) VALUES ('VS Code');
INSERT INTO ProductBrand(Name) VALUES ('React');
INSERT INTO ProductBrand(Name) VALUES ('Typescript');
INSERT INTO ProductBrand(Name) VALUES ('Redis');

INSERT INTO ProductType(Name) VALUES ('Boards');
INSERT INTO ProductType(Name) VALUES ('Hats');
INSERT INTO ProductType(Name) VALUES ('Boots');
INSERT INTO ProductType(Name) VALUES ('Gloves');

INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Angular Speedster Board 2000','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.',200,'images/products/sb-ang1.png',1,1);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Green Angular Board 3000','Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.',150,'images/products/sb-ang2.png',1,1);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Core Board Speed Rush 3','Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.',180,'images/products/sb-core1.png',1,2);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Net Core Super Board','Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.',300,'images/products/sb-core2.png',1,2);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('React Board Super Whizzy Fast','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.',250,'images/products/sb-react1.png',1,4);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Typescript Entry Board','Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.',120,'images/products/sb-ts1.png',1,5);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Core Blue Hat','Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.',10,'images/products/hat-core1.png',2,2);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Green React Woolen Hat','Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.',8,'images/products/hat-react1.png',2,4);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Purple React Woolen Hat','Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.',15,'images/products/hat-react2.png',2,4);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Blue Code Gloves','Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.',18,'images/products/glove-code1.png',4,3);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Green Code Gloves','Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.',15,'images/products/glove-code2.png',4,3);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Purple React Gloves','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa.',16,'images/products/glove-react1.png',4,4);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Green React Gloves','Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.',14,'images/products/glove-react2.png',4,4);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Redis Red Boots','Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.',250,'images/products/boot-redis1.png',3,6);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Core Red Boots','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.',189.99,'images/products/boot-core2.png',3,2);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Core Purple Boots','Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.',199.99,'images/products/boot-core1.png',3,2);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Angular Purple Boots','Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.',150,'images/products/boot-ang2.png',3,1);
INSERT INTO Product(Name,Description,Price,PictureUrl,ProductTypeId,ProductBrandId) VALUES ('Angular Blue Boots','Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.',180,'images/products/boot-ang1.png',3,1);


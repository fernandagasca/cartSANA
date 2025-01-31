--Table -> Products
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Name_Product NVARCHAR(255) NOT NULL, 
    Description NVARCHAR(MAX), 
    Price DECIMAL(10, 2) NOT NULL, 
    Stock INT NOT NULL, 
    Image NVARCHAR(255) 
);


-- Table -> Categories
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Name_Category NVARCHAR(255) NOT NULL 
);

CREATE INDEX IDX_Categories_Name ON Categories (Name_Category);

--Table -> ProductsCategories (Pivot table)
CREATE TABLE ProductsCategories (
    Product_Id INT NOT NULL, 
    Category_Id INT NOT NULL, 
    PRIMARY KEY (Product_Id, Category_Id), 
    FOREIGN KEY (Product_Id) REFERENCES Products(Id), 
    FOREIGN KEY (Category_Id) REFERENCES Categories(Id) 
);

CREATE INDEX IDX_ProductsCategories_Product ON ProductsCategories (Product_Id);
CREATE INDEX IDX_ProductsCategories_Category ON ProductsCategories (Category_Id);

--Table -> Customers
CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Name_Customer NVARCHAR(255) NOT NULL, 
    Email NVARCHAR(255) NOT NULL UNIQUE, 
    Address NVARCHAR(500), 
    Number_Phone NVARCHAR(15) 
);

--Table -> Orders
CREATE TABLE Orders (
    Id_Order INT PRIMARY KEY IDENTITY(1,1), 
    Customer_Id INT NOT NULL, 
    Date_Order DATETIME NOT NULL, 
    FOREIGN KEY (Customer_Id) REFERENCES Customers(Id) 
);

CREATE INDEX IDX_Orders_Customer ON Orders (Customer_Id);

--Table -> OrderDetails (Pivot table)
CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Product_Id INT NOT NULL, 
    Order_Id INT NOT NULL, 
    Quantity INT NOT NULL, 
    FOREIGN KEY (Product_Id) REFERENCES Products(Id), 
    FOREIGN KEY (Order_Id) REFERENCES Orders(Id_Order) 
);

CREATE INDEX IDX_OrderDetails_Product ON OrderDetails (Product_Id);
CREATE INDEX IDX_OrderDetails_Order ON OrderDetails (Order_Id);





-------------------------------------------

-- 1) CATEGORIES
INSERT INTO Categories (Name_Category) VALUES ('Cleansers');
INSERT INTO Categories (Name_Category) VALUES ('Moisturizers');
INSERT INTO Categories (Name_Category) VALUES ('Serums');

-- 2) PRODUCTS
INSERT INTO Products (Name_Product, Description, Price, Stock, Image)
VALUES 
('Gel Limpiador Loto del Sur','Gel limpiador facial suave, ideal para pieles mixtas. Marca Loto del Sur.',29900,50,'gel-limpiador-loto.jpg'),
('Crema Hidratante Koko','Crema hidratante ligera con ingredientes naturales. Marca Koko.',34900,40,'crema-hidratante-koko.jpg'),
('S�rum Vitamina C Esntia','S�rum antioxidante con vitamina C pura. Marca Esntia. Aporta luminosidad.',55900,35,'serum-vitamina-c-esntia.jpg'),


-- 3) PRODUCTS -> CATEGORIES (pivot)
INSERT INTO ProductsCategories (Product_Id, Category_Id) VALUES (1, 1);
INSERT INTO ProductsCategories (Product_Id, Category_Id) VALUES (2, 2);
INSERT INTO ProductsCategories (Product_Id, Category_Id) VALUES (3, 3);


-- 4) CUSTOMERS
INSERT INTO Customers (Name_Customer, Email, Address, Number_Phone)
VALUES ('Maria Gasca', 'maria.gasca@example.com', 'Carrera 9 # 45-23, Neiva', '300000000');

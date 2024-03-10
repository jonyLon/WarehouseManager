create database Warehouse
use Warehouse
go

CREATE TABLE ProductTypes (
    TypeID INT PRIMARY KEY ,
    TypeName VARCHAR(255) NOT NULL
);
go
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY,
    SupplierName VARCHAR(255) NOT NULL
);
go
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(255) NOT NULL,
    TypeID INT,
    SupplierID INT,
    Quantity INT,
    Cost DECIMAL(10, 2),
    SupplyDate DATE,
    FOREIGN KEY (TypeID) REFERENCES ProductTypes(TypeID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);
go

INSERT INTO ProductTypes (TypeID, TypeName) VALUES (1, 'Electronics');
INSERT INTO ProductTypes (TypeID, TypeName) VALUES (2, 'Clothing');
INSERT INTO ProductTypes (TypeID, TypeName) VALUES (3, 'Groceries');

INSERT INTO Suppliers (SupplierID, SupplierName) VALUES (1, 'TechGoods Inc.');
INSERT INTO Suppliers (SupplierID, SupplierName) VALUES (2, 'FashionWear Co.');
INSERT INTO Suppliers (SupplierID, SupplierName) VALUES (3, 'GrocerySupplies Ltd.');

INSERT INTO Products (ProductID, ProductName, TypeID, SupplierID, Quantity, Cost, SupplyDate)
VALUES (1, 'Laptop', 1, 1, 50, 1200.00, '2024-01-10');

INSERT INTO Products (ProductID, ProductName, TypeID, SupplierID, Quantity, Cost, SupplyDate)
VALUES (2, 'T-Shirt', 2, 2, 150, 20.00, '2024-02-15');

INSERT INTO Products (ProductID, ProductName, TypeID, SupplierID, Quantity, Cost, SupplyDate)
VALUES (3, 'Apples', 3, 3, 300, 0.50, '2024-03-05');


CREATE DATABASE Warehouse;
GO

USE Warehouse;


CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactPerson NVARCHAR(100),
    Phone NVARCHAR(20)
);


CREATE TABLE ProductTypes (
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50) NOT NULL
);


CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    TypeID INT FOREIGN KEY REFERENCES ProductTypes(TypeID),
    SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID),
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    SupplyDate DATE NOT NULL
);


INSERT INTO Suppliers (SupplierName, ContactPerson, Phone)
VALUES 
('Supplier A', 'John Doe', '123-456-789'),
('Supplier B', 'Jane Smith', '987-654-321');

INSERT INTO ProductTypes (TypeName)
VALUES 
('Electronics'),
('Furniture'),
('Clothing');

INSERT INTO Products (ProductName, TypeID, SupplierID, Quantity, CostPrice, SupplyDate)
VALUES 
('Laptop', 1, 1, 50, 800.00, '2024-01-15'),
('Table', 2, 2, 20, 150.00, '2024-02-01'),
('T-shirt', 3, 1, 100, 10.00, '2024-03-10');
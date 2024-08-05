CREATE DATABASE RWA;
USE RWA;

-- User Table
CREATE TABLE Usertable (
    UserID INT IDENTITY(100,1) PRIMARY KEY,
    Firstname VARCHAR(255) NOT NULL,
    Lastname VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

-- Order Table
CREATE TABLE Ordertable (
    OrderID INT PRIMARY KEY IDENTITY(100,1),
    UserID INT REFERENCES Usertable(UserID),
    OrderDate DATETIME,
    Status VARCHAR(50)
);

-- OrderItem Table
CREATE TABLE OrderItem (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT REFERENCES Ordertable(OrderID),
    ProductID INT REFERENCES Product(ProductID),
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

-- Payment Table
CREATE TABLE Payment (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT UNIQUE REFERENCES Ordertable(OrderID),
    Amount DECIMAL(10, 2),
    PaymentDate DATETIME,
    PaymentMethod VARCHAR(255)
);

-- Shipment Table
CREATE TABLE Shipment (
    ShipmentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT UNIQUE REFERENCES Ordertable(OrderID),
    ShipmentDate DATETIME NOT NULL,
    TrackingNumber VARCHAR(255)
);

-- Cart Table
CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT REFERENCES Usertable(UserID)
);

-- CartItem Table
CREATE TABLE CartItem (
    CartItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT REFERENCES Cart(CartID),
    ProductID INT REFERENCES Product(ProductID),
    Quantity INT NOT NULL
);

-- Wishlist Table
CREATE TABLE Wishlist (
    WishlistID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE REFERENCES Usertable(UserID)
);

-- WishlistItem Table
CREATE TABLE WishlistItem (
    WishlistItemID INT IDENTITY(1,1) PRIMARY KEY,
    WishlistID INT REFERENCES Wishlist(WishlistID),
    ProductID INT REFERENCES Product(ProductID)
);

-- Product Table
CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CategoryID INT REFERENCES Category(CategoryID),
    InventoryID INT UNIQUE REFERENCES Inventory(InventoryID)
);

-- Review Table
CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT REFERENCES Usertable(UserID),
    ProductID INT REFERENCES Product(ProductID),
    Rating INT CHECK (Rating >= 1 AND Rating <= 5)
);

-- Category Table
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(255) NOT NULL
);

-- Inventory Table
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    StockQuantity INT NOT NULL,
    RestockDate DATETIME
);

-- Address Table
CREATE TABLE Address (
    UserID INT REFERENCES Usertable(UserID),
    HouseNumber INT NOT NULL,
    City VARCHAR(255),
    State VARCHAR(255),
    PRIMARY KEY (UserID, HouseNumber)
);

-- Notification Table
CREATE TABLE Notification (
    UserID INT REFERENCES Usertable(UserID),
    Message VARCHAR(1000) NOT NULL,
    Date DATETIME NOT NULL,
    PRIMARY KEY (UserID, Date, Message)
);


-- Insert entries into Usertable
INSERT INTO Usertable (Firstname, Lastname, Email) VALUES 
('John', 'Doe', 'john.doe@example.com'),
('Jane', 'Smith', 'jane.smith@example.com'),
('Alice', 'Johnson', 'alice.johnson@example.com'),
('Bob', 'Brown', 'bob.brown@example.com'),
('Charlie', 'Davis', 'charlie.davis@example.com'),
('Eve', 'Wilson', 'eve.wilson@example.com');

-- Insert entries into Category
INSERT INTO Category (CategoryName) VALUES 
('Electronics'),
('Books'),
('Clothing'),
('Home Appliances'),
('Toys'),
('Sports Equipment');

-- Insert entries into Inventory
INSERT INTO Inventory (StockQuantity, RestockDate) VALUES 
(100, '2024-08-01'),
(50, '2024-08-05'),
(200, '2024-08-10'),
(150, '2024-08-15'),
(300, '2024-08-20'),
(80, '2024-08-25');

-- Insert entries into Product
INSERT INTO Product (ProductName, Price, CategoryID, InventoryID) VALUES 
('Laptop', 999.99, 1, 1),
('Novel', 19.99, 2, 2),
('T-shirt', 9.99, 3, 3),
('Blender', 49.99, 4, 4),
('Action Figure', 14.99, 5, 5),
('Basketball', 29.99, 6, 6);

-- Insert entries into Ordertable
INSERT INTO Ordertable (UserID, OrderDate, Status) VALUES 
(100, '2024-08-01', 'Shipped'),
(101, '2024-08-02', 'Processing'),
(102, '2024-08-03', 'Delivered'),
(103, '2024-08-04', 'Cancelled'),
(104, '2024-08-05', 'Processing'),
(105, '2024-08-06', 'Shipped');

-- Insert entries into OrderItem
INSERT INTO OrderItem (OrderID, ProductID, Quantity, Price) VALUES 
(100, 1, 1, 999.99),
(101, 2, 2, 39.98),
(102, 3, 3, 29.97),
(103, 4, 1, 49.99),
(104, 5, 2, 29.98),
(105, 6, 1, 29.99);

-- Insert entries into Payment
INSERT INTO Payment (OrderID, Amount, PaymentDate, PaymentMethod) VALUES 
(100, 999.99, '2024-08-01', 'Credit Card'),
(101, 39.98, '2024-08-02', 'PayPal'),
(102, 29.97, '2024-08-03', 'Debit Card'),
(103, 49.99, '2024-08-04', 'Credit Card'),
(104, 29.98, '2024-08-05', 'PayPal'),
(105, 29.99, '2024-08-06', 'Debit Card');

-- Insert entries into Shipment
INSERT INTO Shipment (OrderID, ShipmentDate, TrackingNumber) VALUES 
(100, '2024-08-02', 'TRACK123'),
(101, '2024-08-03', 'TRACK124'),
(102, '2024-08-04', 'TRACK125'),
(103, '2024-08-05', 'TRACK126'),
(104, '2024-08-06', 'TRACK127'),
(105, '2024-08-07', 'TRACK128');

-- Insert entries into Cart
INSERT INTO Cart (UserID) VALUES 
(100),
(101),
(102),
(103),
(104),
(105);

-- Insert entries into CartItem
INSERT INTO CartItem (CartID, ProductID, Quantity) VALUES 
(1, 1, 1),
(2, 2, 2),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(6, 6, 1);

-- Insert entries into Wishlist
INSERT INTO Wishlist (UserID) VALUES 
(100),
(101),
(102),
(103),
(104),
(105);

-- Insert entries into WishlistItem
INSERT INTO WishlistItem (WishlistID, ProductID) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);

-- Insert entries into Review
INSERT INTO Review (UserID, ProductID, Rating) VALUES 
(100, 1, 5),
(101, 2, 4),
(102, 3, 3),
(103, 4, 5),
(104, 5, 4),
(105, 6, 5);

-- Insert entries into Address
INSERT INTO Address (UserID, HouseNumber, City, State) VALUES 
(100, 123, 'New York', 'NY'),
(101, 456, 'Los Angeles', 'CA'),
(102, 789, 'Chicago', 'IL'),
(103, 101, 'Houston', 'TX'),
(104, 202, 'Phoenix', 'AZ'),
(105, 303, 'Philadelphia', 'PA');

-- Insert entries into Notification
INSERT INTO Notification (UserID, Message, Date) VALUES 
(100, 'Your order has been shipped.', '2024-08-01'),
(101, 'Your payment was successful.', '2024-08-02'),
(102, 'Your order has been delivered.', '2024-08-03'),
(103, 'Your order was cancelled.', '2024-08-04');


-- Select all from Usertable
SELECT * FROM Usertable;

-- Select all from Ordertable
SELECT * FROM Ordertable;

-- Select all from OrderItem
SELECT * FROM OrderItem;

-- Select all from Payment
SELECT * FROM Payment;

-- Select all from Shipment
SELECT * FROM Shipment;

-- Select all from Cart
SELECT * FROM Cart;

-- Select all from CartItem
SELECT * FROM CartItem;

-- Select all from Wishlist
SELECT * FROM Wishlist;

-- Select all from WishlistItem
SELECT * FROM WishlistItem;

-- Select all from Product
SELECT * FROM Product;

-- Select all from Review
SELECT * FROM Review;

-- Select all from Category
SELECT * FROM Category;

-- Select all from Inventory
SELECT * FROM Inventory;

-- Select all from Address
SELECT * FROM Address;

-- Select all from Notification
SELECT * FROM Notification;

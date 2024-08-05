use RWA;

SELECT o.OrderID, o.OrderDate, o.Status, SUM(oi.Quantity * oi.Price) AS TotalAmount
FROM Ordertable o
JOIN Usertable u ON o.UserID = u.UserID
JOIN OrderItem oi ON o.OrderID = oi.OrderID
WHERE u.UserID = 100 
GROUP BY o.OrderID, o.OrderDate, o.Status;

SELECT oi.OrderID, p.ProductName, oi.Quantity, oi.Price, (oi.Quantity * oi.Price) AS TotalPrice
FROM OrderItem oi
JOIN Product p ON oi.ProductID = p.ProductID
WHERE oi.OrderID = 100;  

SELECT r.ReviewID, p.ProductName, r.Rating
FROM Review r
JOIN Product p ON r.ProductID = p.ProductID
WHERE r.UserID = 100;  -- Replace with specific Customer ID

SELECT p.ProductID, p.ProductName, SUM(oi.Quantity) AS TotalSales, COUNT(oi.OrderID) AS NumberOfOrders
FROM OrderItem oi
JOIN Product p ON oi.ProductID = p.ProductID
GROUP BY p.ProductID, p.ProductName;

SELECT YEAR(o.OrderDate) AS Year, MONTH(o.OrderDate) AS Month, SUM(oi.Quantity * oi.Price) AS TotalSales, COUNT(o.OrderID) AS NumberOfOrders
FROM Ordertable o
JOIN OrderItem oi ON o.OrderID = oi.OrderID
GROUP BY YEAR(o.OrderDate), MONTH(o.OrderDate);

SELECT u.UserID, u.Firstname, u.Lastname, AVG(oi.Quantity * oi.Price) AS AverageOrderValue
FROM Usertable u
JOIN Ordertable o ON u.UserID = o.UserID
JOIN OrderItem oi ON o.OrderID = oi.OrderID
GROUP BY u.UserID, u.Firstname, u.Lastname;

SELECT c.CategoryID, c.CategoryName, COUNT(o.OrderID) AS NumberOfOrders, SUM(oi.Quantity * oi.Price) AS TotalSales
FROM OrderItem oi
JOIN Product p ON oi.ProductID = p.ProductID
JOIN Category c ON p.CategoryID = c.CategoryID
JOIN Ordertable o ON oi.OrderID = o.OrderID
GROUP BY c.CategoryID, c.CategoryName;

SELECT u.UserID, u.Firstname, u.Lastname, SUM(oi.Quantity * oi.Price) AS TotalOrderValue
FROM Usertable u
JOIN Ordertable o ON u.UserID = o.UserID
JOIN OrderItem oi ON o.OrderID = oi.OrderID
GROUP BY u.UserID, u.Firstname, u.Lastname
HAVING SUM(oi.Quantity * oi.Price) > 1000;

SELECT o.OrderID, AVG(DATEDIFF(day, o.OrderDate, s.ShipmentDate)) AS AverageFulfillmentTime
FROM Ordertable o
JOIN Shipment s ON o.OrderID = s.OrderID
GROUP BY o.OrderID;

SELECT p.ProductID, p.ProductName
FROM Product p
LEFT JOIN Review r ON p.ProductID = r.ProductID
WHERE r.ProductID IS NULL;

WITH CartsWithOrders AS (
    SELECT c.CartID
    FROM Cart c
    JOIN Ordertable o ON c.UserID = o.UserID
)
SELECT COUNT(c.CartID) AS TotalCarts,
       (COUNT(c.CartID) - COUNT(cwo.CartID)) * 100.0 / COUNT(c.CartID) AS AbandonmentRate
FROM Cart c
LEFT JOIN CartsWithOrders cwo ON c.CartID = cwo.CartID;

SELECT p.ProductID, p.ProductName, COUNT(wi.WishlistItemID) AS WishlistCount
FROM WishlistItem wi
JOIN Product p ON wi.ProductID = p.ProductID
GROUP BY p.ProductID, p.ProductName
ORDER BY WishlistCount DESC;

SELECT o.Status, COUNT(*) AS NumberOfOrders
FROM Ordertable o
GROUP BY o.Status;

SELECT COUNT(*) AS OrdersWithDiscounts, 
       SUM(Amount) AS TotalDiscountAmount
FROM Payment
WHERE Amount < (SELECT SUM(oi.Quantity * oi.Price) FROM OrderItem oi WHERE oi.OrderID = Payment.OrderID);

SELECT p.ProductID, p.ProductName, i.StockQuantity, i.RestockDate
FROM Product p
JOIN Inventory i ON p.InventoryID = i.InventoryID;

SELECT DATEPART(HOUR, o.OrderDate) AS PurchaseHour, COUNT(*) AS NumberOfOrders
FROM Ordertable o
GROUP BY DATEPART(HOUR, o.OrderDate)
ORDER BY NumberOfOrders DESC;

SELECT c.CategoryID, c.CategoryName, SUM(oi.Quantity * oi.Price) AS TotalSales, AVG(oi.Quantity * oi.Price) AS AverageOrderValue
FROM OrderItem oi
JOIN Product p ON oi.ProductID = p.ProductID
JOIN Category c ON p.CategoryID = c.CategoryID
GROUP BY c.CategoryID, c.CategoryName;

SELECT u.UserID, u.Firstname, u.Lastname, SUM(oi.Quantity * oi.Price) AS LifetimeValue
FROM Usertable u
JOIN Ordertable o ON u.UserID = o.UserID
JOIN OrderItem oi ON o.OrderID = oi.OrderID
GROUP BY u.UserID, u.Firstname, u.Lastname;

SELECT a.State, p.ProductID, p.ProductName, COUNT(oi.OrderID) AS NumberOfOrders
FROM Ordertable o
JOIN OrderItem oi ON o.OrderID = oi.OrderID
JOIN Product p ON oi.ProductID = p.ProductID
JOIN Address a ON o.UserID = a.UserID
GROUP BY a.State, p.ProductID, p.ProductName
ORDER BY NumberOfOrders DESC;

SELECT AVG(CASE WHEN Amount < (SELECT SUM(oi.Quantity * oi.Price) FROM OrderItem oi WHERE oi.OrderID = Payment.OrderID) THEN 1 ELSE 0 END) AS DiscountUsageRate
FROM Payment;

WITH CustomerOrderCount AS (
    SELECT UserID, COUNT(OrderID) AS NumberOfOrders
    FROM Ordertable
    GROUP BY UserID
)
SELECT AVG(CASE WHEN NumberOfOrders > 1 THEN 1 ELSE 0 END) AS RepeatPurchaseRate
FROM CustomerOrderCount;

SELECT p.ProductID, p.ProductName, AVG(r.Rating) AS AverageRating
FROM Product p
JOIN Review r ON p.ProductID = r.ProductID
GROUP BY p.ProductID, p.ProductName
ORDER BY AverageRating DESC;

SELECT s.TrackingNumber, AVG(DATEDIFF(day, o.OrderDate, s.ShipmentDate)) AS AverageShippingTime
FROM Shipment s
JOIN Ordertable o ON s.OrderID = o.OrderID
GROUP BY s.TrackingNumber
ORDER BY AverageShippingTime;

WITH AbandonedCarts AS (
    SELECT c.CartID
    FROM Cart c
    LEFT JOIN Ordertable o ON c.UserID = o.UserID
    WHERE o.OrderID IS NULL
)
SELECT COUNT(*) AS AbandonedCarts, 
       SUM(CASE WHEN o.OrderID IS NOT NULL THEN 1 ELSE 0 END) AS RecoveredCarts
FROM AbandonedCarts ac
LEFT JOIN Ordertable o ON ac.CartID = o.UserID;

SELECT u.UserID, u.Firstname, u.Lastname, 
       (COUNT(o.OrderID) + COUNT(r.ReviewID) + COUNT(w.WishlistID)) AS TotalInteractions
FROM Usertable u
LEFT JOIN Ordertable o ON u.UserID = o.UserID
LEFT JOIN Review r ON u.UserID = r.UserID
LEFT JOIN Wishlist w ON u.UserID = w.UserID
GROUP BY u.UserID, u.Firstname, u.Lastname;

SELECT YEAR(o.OrderDate) AS Year, MONTH(o.OrderDate) AS Month, SUM(oi.Quantity * oi.Price) AS TotalSales
FROM Ordertable o
JOIN OrderItem oi ON o.OrderID = oi.OrderID
GROUP BY YEAR(o.OrderDate), MONTH(o.OrderDate)
ORDER BY Year, Month;

WITH SalesData AS (
    SELECT p.ProductID, SUM(oi.Quantity) AS TotalSales
    FROM OrderItem oi
    JOIN Product p ON oi.ProductID = p.ProductID
    GROUP BY p.ProductID
),
InventoryData AS (
    SELECT p.ProductID, i.StockQuantity AS AverageInventory
    FROM Product p
    JOIN Inventory i ON p.InventoryID = i.InventoryID
)
SELECT sd.ProductID, p.ProductName, sd.TotalSales, id.AverageInventory,
       CASE 
           WHEN id.AverageInventory = 0 THEN NULL
           ELSE sd.TotalSales / id.AverageInventory 
       END AS InventoryTurnoverRate
FROM SalesData sd
JOIN InventoryData id ON sd.ProductID = id.ProductID
JOIN Product p ON sd.ProductID = p.ProductID;

SELECT PaymentMethod, COUNT(*) AS NumberOfTransactions
FROM Payment
GROUP BY PaymentMethod
ORDER BY NumberOfTransactions DESC;


WITH ProductPairs AS (
    SELECT oi1.ProductID AS ProductID1, oi2.ProductID AS ProductID2, COUNT(*) AS PairCount
    FROM OrderItem oi1
    JOIN OrderItem oi2 ON oi1.OrderID = oi2.OrderID AND oi1.ProductID < oi2.ProductID
    GROUP BY oi1.ProductID, oi2.ProductID
)
SELECT p1.ProductID AS ProductID1, p1.ProductName AS ProductName1, 
       p2.ProductID AS ProductID2, p2.ProductName AS ProductName2, 
       pp.PairCount
FROM ProductPairs pp
JOIN Product p1 ON pp.ProductID1 = p1.ProductID
JOIN Product p2 ON pp.ProductID2 = p2.ProductID
ORDER BY pp.PairCount DESC;









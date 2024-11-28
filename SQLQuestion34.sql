CREATE DATABASE ProductDB
GO

USE ProductDB;
GO

CREATE TABLE categories
(
	categoryId INT IDENTITY PRIMARY KEY NOT NULL,
	categoryName VARCHAR (100)
	
)
GO;

CREATE TABLE products
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	name VARCHAR (100),
	price DECIMAL(18,2),
	categoryId INT NOT NULL,
	FOREIGN KEY (categoryId) REFERENCES categories(categoryId),
)
GO;

SELECT 
    p.name AS productName, 
    p.price, 
    c.categoryName
FROM products p
LEFT JOIN categories c
ON p.categoryId = c.categoryId;
GO;

CREATE PROCEDURE sp_UpdateProductPrice
    @ProductId INT,
    @NewPrice DECIMAL(18, 2)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Products WHERE Id = @ProductId)
    BEGIN
        UPDATE products
        SET price = @NewPrice
        WHERE id = @ProductId;

        PRINT 'Update Successful';
    END
    ELSE
    BEGIN
        PRINT 'Product Not Found';
    END
END;

EXEC sp_UpdateProductPrice @ProductId = 1, @NewPrice = 99.99;

SELECT * FROM products;


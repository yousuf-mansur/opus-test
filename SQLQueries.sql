USE ProductTestDB;
GO;
-- Question No-2(1)
SELECT * 
FROM products
WHERE price > 50
ORDER BY name ASC;


-- Question No-2(2)
-- Update
UPDATE products
SET price = 75
WHERE id = 6 AND EXISTS (SELECT 1 FROM products WHERE id = 6);
GO;

SELECT * FROM products;





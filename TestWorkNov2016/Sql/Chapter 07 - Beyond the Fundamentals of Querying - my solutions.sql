USE TSQL2012;
GO


-- preparation: 
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
GO

CREATE TABLE dbo.Orders
(
  orderid   INT        NOT NULL,
  orderdate DATE       NOT NULL,
  empid     INT        NOT NULL,
  custid    VARCHAR(5) NOT NULL,
  qty       INT        NOT NULL,
  CONSTRAINT PK_Orders PRIMARY KEY(orderid)
);

INSERT INTO dbo.Orders(orderid, orderdate, empid, custid, qty)
VALUES
  (30001, '20070802', 3, 'A', 10),
  (10001, '20071224', 2, 'A', 12),
  (10005, '20071224', 1, 'B', 20),
  (40001, '20080109', 2, 'A', 40),
  (10006, '20080118', 1, 'C', 14),
  (20001, '20080212', 2, 'B', 12),
  (40005, '20090212', 3, 'A', 10),
  (20002, '20090216', 1, 'C', 20),
  (30003, '20090418', 2, 'B', 15),
  (30004, '20070418', 3, 'C', 22),
  (30007, '20090907', 3, 'D', 30);

SELECT * FROM dbo.Orders;




-- All exercises for this chapter will involve querying the dbo.Orders
-- table in the TSQL2012 database that you created and populated 
-- earlier by running the code in Listing 7-1



-- 1
-- Write a query against the dbo.Orders table that computes for each
-- customer order, both a rank and a dense rank,
-- partitioned by custid, ordered by qty 

-- Desired output:
select c.custid, c.orderid, c.qty, 
(rank() over (partition by c.custid order by c.qty)) as rnk, 
(dense_rank() over (partition by c.custid order by c.qty)) as drnk
from [dbo].Orders as c




-- 2
-- Write a query against the dbo.Orders table that computes for each
-- customer order:
-- * the difference between the current order quantity
--   and the customer's previous order quantity
-- * the difference between the current order quantity
--   and the customer's next order quantity.

-- Desired output:
--custid orderid     qty         diffprev    diffnext



select c.custid, c.orderid, c.qty, 
c.qty - lag(c.qty) over (partition by c.custid order by  c.orderdate, c.orderid) as diffprev, 
c.qty - lead(c.qty) over (partition by c.custid order by c.orderdate, c.orderid) as diffnext
from [dbo].Orders as c







-- 3
-- Write a query against the dbo.Orders table that returns a row for each
-- employee, a column for each order year, and the count of orders
-- for each employee and order year
-- Tables involved: TSQL2012 database, dbo.Orders table


-- without pivot operator:

SELECT empid, 
  count(CASE WHEN year(orderdate) = '2007' THEN orderid END) AS cnt2007,
  count(CASE WHEN year(orderdate) = '2008' THEN orderid END) AS cnt2008,
  count(CASE WHEN year(orderdate) = '2009' THEN orderid END) AS cnt2009
FROM dbo.Orders
GROUP BY empid;


-- with pivot operator:

SELECT empid, [2007] as cnt2007, [2008] as cnt2008, [2009] as cnt2009
FROM
	(SELECT empid, orderid, year(orderdate) as yr 
	FROM dbo.Orders) AS D
PIVOT(count(orderid) FOR yr IN([2007], [2008], [2009])) AS P;









-- 4
-- Run the following code to create and populate the EmpYearOrders table:
USE TSQL2012;

IF OBJECT_ID('dbo.EmpYearOrders', 'U') IS NOT NULL DROP TABLE dbo.EmpYearOrders;

CREATE TABLE dbo.EmpYearOrders
(
  empid INT NOT NULL
    CONSTRAINT PK_EmpYearOrders PRIMARY KEY,
  cnt2007 INT NULL,
  cnt2008 INT NULL,
  cnt2009 INT NULL
);

INSERT INTO dbo.EmpYearOrders(empid, cnt2007, cnt2008, cnt2009)
  SELECT empid, [2007] AS cnt2007, [2008] AS cnt2008, [2009] AS cnt2009
  FROM (SELECT empid, YEAR(orderdate) AS orderyear
        FROM dbo.Orders) AS D
    PIVOT(COUNT(orderyear)
          FOR orderyear IN([2007], [2008], [2009])) AS P;

SELECT * FROM dbo.EmpYearOrders;


-- Write a query against the EmpYearOrders table that unpivots
-- the data, returning a row for each employee and order year
-- with the number of orders
-- Exclude rows where the number of orders is 0
-- (in our example, employee 3 in year 2008)

-- Desired output:
--empid       orderyear   numorders
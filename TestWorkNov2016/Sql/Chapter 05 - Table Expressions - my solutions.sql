USE TSQL2012;
GO


-- 1-1
-- Write a query that returns the maximum order date for each employee
-- Tables involved: TSQL2012 database, Sales.Orders table


select o.empid, max(o.orderdate) as maxorderdate
from [Sales].Orders as o
group by o.empid
order by o.empid




-- 1-2
-- Encapsulate the query from exercise 1-1 in a derived table
-- Write a join query between the derived table and the Sales.Orders
-- table to return the Sales.Orders with the maximum order date for 
-- each employee
-- Tables involved: Sales.Orders


select derivedTabl.empid, derivedTabl.maxorderdate, o2.orderid, o2.custid
from
	(select o1.empid, max(o1.orderdate) as maxorderdate
	from [Sales].Orders as o1
	group by o1.empid) as derivedTabl
inner join [Sales].Orders as o2
on 
	derivedTabl.empid = o2.empid and 
	derivedTabl.maxorderdate = o2.orderdate




	
-- 2-1
-- Write a query that calculates a row number for each order
-- based on orderdate, orderid ordering
-- Tables involved: Sales.Orders

select o.orderid, o.orderdate, o.custid, o.empid,
	 ROW_NUMBER() over (order by o.orderdate, o.orderid) as rownum
from [Sales].Orders as o
order by o.orderid





-- 2-2
-- Write a query that returns rows with row numbers 11 through 20
-- based on the row number definition in exercise 2-1
-- Use a CTE to encapsulate the code from exercise 2-1
-- Tables involved: Sales.Orders


with rnms as
(select o.orderid, o.orderdate, o.custid, o.empid,
	 ROW_NUMBER() over (order by o.orderdate, o.orderid) as rownum
from [Sales].Orders as o)
select rnms.orderid, rnms.orderdate, rnms.custid, rnms.empid, rnms.rownum
from rnms
where rnms.rownum > 10 and rnms.rownum <= 20







-- 3 (Optional, Advanced)
-- Write a solution using a recursive CTE that returns the 
-- management chain leading to Zoya Dolgopyatova (employee ID 9)
-- Tables involved: HR.Employees

-- not mine, from book:

WITH EmpsCTE AS
(
  SELECT empid, mgrid, firstname, lastname
  FROM HR.Employees
  WHERE empid = 9
  
  UNION ALL
  
  SELECT P.empid, P.mgrid, P.firstname, P.lastname
  FROM EmpsCTE AS C
    JOIN HR.Employees AS P
      ON C.mgrid = P.empid
)
SELECT empid, mgrid, firstname, lastname
FROM EmpsCTE;






-- 4-1
-- Create a view that returns the total qty
-- for each employee and year
-- Tables involved: Sales.Orders and Sales.OrderDetails

IF OBJECT_ID('Sales.VEmpOrders') IS NOT NULL
DROP VIEW Sales.VEmpOrders;
GO

create view Sales.VEmpOrders
AS
(
	select o.empid, year(o.orderdate) as orderyear, sum(od.qty) as total_qty
	from Sales.Orders as o
	join Sales.OrderDetails as od
	on o.orderid = od.orderid
	group by o.empid, year(o.orderdate) 
);
go

select ve.empid, ve.orderyear, ve.total_qty 
from Sales.VEmpOrders as ve
order by ve.empid, ve.orderyear







-- 4-2 (Optional, Advanced)
-- Write a query against Sales.VEmpOrders
-- that returns the running qty for each employee and year
-- Tables involved: TSQL2012 database, Sales.VEmpOrders view


IF OBJECT_ID('Sales.VEmpOrders') IS NOT NULL
DROP VIEW Sales.VEmpOrders;
GO

create view Sales.VEmpOrders
AS
(
	select o.empid,  year(o.orderdate) as orderyear, sum(od.qty) as total_qty
	from Sales.Orders as o
	join Sales.OrderDetails as od
	on o.orderid = od.orderid
	group by o.empid, year(o.orderdate) 
);
go

select ve.empid, ve.orderyear, ve.total_qty, 
	(select sum(ve2.total_qty) 
	from Sales.VEmpOrders as ve2 
	where ve2.orderyear <= ve.orderyear and ve.empid = ve2.empid) as runqty
from Sales.VEmpOrders as ve
order by ve.empid, ve.orderyear





-- 5-1
-- Create an inline function that accepts as inputs
-- a supplier id (@supid AS INT), 
-- and a requested number of products (@n AS INT)
-- The function should return @n products with the highest unit prices
-- that are supplied by the given supplier id
-- Tables involved: Production.Products

IF OBJECT_ID('Production.TopProducts') IS NOT NULL
DROP function Production.TopProducts;
GO

create function Production.TopProducts (@supid AS INT, @n AS INT)
returns table
AS
return
	select top(@n) p.productid,  p.productname, p.unitprice
	from Production.Products as p
	where p.supplierid = @supid
	order by p.unitprice desc
go

select * from Production.TopProducts(5, 2);








-- 5-1
-- Create an inline function that accepts as inputs
-- a supplier id (@supid AS INT), 
-- and a requested number of products (@n AS INT)
-- The function should return @n products with the highest unit prices
-- that are supplied by the given supplier id
-- Tables involved: Production.Products

IF OBJECT_ID('Production.TopProducts') IS NOT NULL
DROP function Production.TopProducts;
GO

create function Production.TopProducts (@supid AS INT, @n AS INT)
returns table
AS
return
	select top(@n) p.productid,  p.productname, p.unitprice
	from Production.Products as p
	where p.supplierid = @supid
	order by p.unitprice desc
go


SELECT distinct s.supplierid, s.companyname, p.productid,  p.productname, p.unitprice
FROM Production.Suppliers AS s
CROSS APPLY
	Production.TopProducts(s.supplierid, 2) as p
order by s.companyname;








--  code for cleanup:
IF OBJECT_ID('Sales.VEmpOrders') IS NOT NULL
  DROP VIEW Sales.VEmpOrders;
IF OBJECT_ID('Production.TopProducts') IS NOT NULL
  DROP FUNCTION Production.TopProducts;
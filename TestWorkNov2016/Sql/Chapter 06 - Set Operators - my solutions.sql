USE TSQL2012;
GO


-- 1
-- Write a query that generates a virtual auxiliary table of 10 numbers
-- in the range 1 through 10
-- Tables involved: no table


select top (10) row_number() 
over (order by c.custid)
from [Sales].Customers as c 


-- not mine, from book:
SELECT 1 AS n
UNION ALL SELECT 2
UNION ALL SELECT 3
UNION ALL SELECT 4
UNION ALL SELECT 5
UNION ALL SELECT 6
UNION ALL SELECT 7
UNION ALL SELECT 8
UNION ALL SELECT 9
UNION ALL SELECT 10;





-- 2
-- Write a query that returns customer and employee pairs 
-- that had order activity in January 2008 but not in February 2008
-- Tables involved: TSQL2012 database, Orders table

select o.custid, o.empid
	from Sales.Orders as o 
	where o.orderdate >= '20080101' and 
		 o.orderdate <= eomonth('20080101')

union

select o1.custid, o1.empid
	from Sales.Orders as o1 
	where o1.orderdate < '20080201' and 
		 o1.orderdate > eomonth('20080201')






-- 3
-- Write a query that returns customer and employee pairs 
-- that had order activity in both January 2008 and February 2008
-- Tables involved: TSQL2012 database, Orders table

select o.custid, o.empid
	from Sales.Orders as o 
	where o.orderdate >= '20080101' and 
		 o.orderdate <= eomonth('20080101')

intersect

select o1.custid, o1.empid
	from Sales.Orders as o1 
	where o1.orderdate >= '20080201' and 
		 o1.orderdate <= eomonth('20080201')





-- 4
-- Write a query that returns customer and employee pairs 
-- that had order activity in both January 2008 and February 2008
-- but not in 2007
-- Tables involved: TSQL2012 database, Orders table

(
select o.custid, o.empid
	from Sales.Orders as o 
	where o.orderdate >= '20080101' and 
		 o.orderdate <= eomonth('20080101')

intersect

select o1.custid, o1.empid
	from Sales.Orders as o1 
	where o1.orderdate >= '20080201' and 
		 o1.orderdate <= eomonth('20080201')
		 )

except 

select o2.custid, o2.empid
	from Sales.Orders as o2 
	where year(o2.orderdate) = 2007





-- 5 (Optional, Advanced)
-- You are given the following query:
SELECT country, region, city
FROM HR.Employees

UNION ALL

SELECT country, region, city
FROM Production.Suppliers;

-- You are asked to add logic to the query 
-- such that it would guarantee that the rows from Employees
-- would be returned in the output before the rows from Suppliers,
-- and within each segment, the rows should be sorted
-- by country, region, city
-- Tables involved: TSQL2012 database, Employees and Suppliers tables


select * from
(SELECT 1 as sortcol, a.country, a.region, a.city
		FROM HR.Employees as a

UNION ALL

SELECT 2 as sortcol, b.country, b.region, b.city
FROM Production.Suppliers as b) as A
order by A.sortcol, A.country, A.region, A.city
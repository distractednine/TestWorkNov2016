USE TSQL2012;
GO

-- 1
-- 1-1
-- Write a query that generates 5 copies out of each employee row
-- Tables involved: TSQL2012 database, Employees and Nums tables

select e.empid, e.firstname, e.lastname, n.n
from [HR].Employees as e 
	cross join Nums as n where n.n <= 5;



-- 1-2 (Optional, Advanced)
-- Write a query that returns a row for each employee and day 
-- in the range June 12, 2009 – June 16 2009.
-- Tables involved: TSQL2012 database, Employees and Nums tables

select e.empid,  
	 dateadd(day, n.n, '20090611')
from [HR].Employees as e 
	cross join Nums as n where n.n <= 5
order by e.empid




-- 2
-- Return US customers, and for each customer the total number of orders 
-- and total quantities.
-- Tables involved: TSQL2012 database, Customers, Orders and OrderDetails tables

select 
	top 1000 
		c.custid, 
		count(distinct o.orderid) as numorders,
		sum(od.qty) as totalqty
from ([Sales].Customers as c
	inner join [Sales].Orders as o 
	on c.custid = o.custid) 
		inner join [Sales].OrderDetails as od 
			on o.orderid = od.orderid
where lower(c.country) = 'usa'
group by c.custid



-- 3
-- Return customers and their orders including customers who placed no orders
-- Tables involved: TSQL2012 database, Customers and Orders tables

select 
	top 1000 
		c.custid, 
		c.companyname, 
		o.orderid,
		o.orderdate
from ([Sales].Customers as c
	left outer join [Sales].Orders as o 
	on c.custid = o.custid) 



	
-- 4
-- Return customers who placed no orders
-- Tables involved: TSQL2012 database, Customers and Orders tables

select 
	top 1000 
		c.custid, 
		c.companyname
from ([Sales].Customers as c
	left outer join [Sales].Orders as o 
	on c.custid = o.custid) 
where o.orderid is null




-- 5
-- Return customers with orders placed on Feb 12, 2007 along with their orders
-- Tables involved: TSQL2012 database, Customers and Orders tables

select 
	top 1000 
		c.custid, 
		c.companyname, 
		o.orderid,
		o.orderdate
from ([Sales].Customers as c
	inner join [Sales].Orders as o 
	on c.custid = o.custid) 
where o.orderdate >= '20070212' and o.orderdate < '20070213'




-- 6 (Optional, Advanced)
-- Return customers with orders placed on Feb 12, 2007 along with their orders
-- Also return customers who didn't place orders on Feb 12, 2007 
-- Tables involved: TSQL2012 database, Customers and Orders tables

select 
	top 1000 
		c.custid, 
		c.companyname, 
		o.orderid,
		o.orderdate
from ([Sales].Customers as c
	left outer join [Sales].Orders as o 
	on (
		c.custid = o.custid and 
		(o.orderdate >= '20070212' and o.orderdate < '20070213'))
	)
	


-- 7 (Optional, Advanced)
-- Return all customers, and for each return a Yes/No value
-- depending on whether the customer placed an order on Feb 12, 2007
-- Tables involved: TSQL2012 database, Customers and Orders tables

select distinct
	top 10000
		c.custid, 
		c.companyname, 
		case 
			when o.orderdate is null then 'No' 
			else 'Yes' 
		end as	HasOrderOn20070212
from ([Sales].Customers as c
	left outer join [Sales].Orders as o 
	on (
		c.custid = o.custid and 
		(o.orderdate >= '20070212' and o.orderdate < '20070213'))
	)
order by c.custid
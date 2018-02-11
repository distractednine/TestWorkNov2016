USE TSQL2012;
GO

-- 1 
-- Write a query that returns all orders placed on the last day of
-- activity that can be found in the Orders table
-- Tables involved: TSQL2012 database, Orders table

select o.orderid, o.orderdate, o.custid, o.empid
from [Sales].Orders as o
where o.orderdate = (select max(orderdate) from [Sales].Orders)


-- 2 (Optional, Advanced)
-- Write a query that returns all orders placed
-- by the customer(s) who placed the highest number of orders
-- * Note: there may be more than one customer
--   with the same number of orders
-- Tables involved: TSQL2012 database, Orders table

select oo.orderid, oo.orderdate, oo.custid, oo.empid
from [Sales].Orders as oo
where oo.custid in
	(select outr.custid
	from [Sales].Orders as outr
	group by outr.custid
	having count(outr.custid) = 		
		(
		select max(inn.numOrders) from
			(
			select o.custid, count(o.custid) as numOrders
			from [Sales].Orders as o
			group by o.custid
			) 
		as inn
		)
	)

-- from book (not mine):

SELECT custid, orderid, orderdate, empid
FROM Sales.Orders
WHERE custid IN
  (SELECT TOP (1) WITH TIES O.custid
   FROM Sales.Orders AS O
   GROUP BY O.custid
   ORDER BY COUNT(*) DESC);



-- 3
-- Write a query that returns employees
-- who did not place orders on or after May 1st, 2008
-- Tables involved: TSQL2012 database, Employees and Orders tables

select e.empid, e.firstName, e.lastname
from [HR].Employees as e 
where e.empid not in 
	(select distinct o.empid 
	from Sales.Orders as o 
	where o.orderdate >= '20080501')




-- 4
-- Write a query that returns
-- countries where there are customers but not employees
-- Tables involved: TSQL2012 database, Customers and Employees tables

select distinct s.country 
from [Sales].Customers as s
where s.country not in 
	(select distinct e.country 
	from [Hr].Employees as e)
order by s.country





-- 5
-- Write a query that returns for each customer
-- all orders placed on the customer's last day of activity
-- Tables involved: TSQL2012 database, Orders table

select outr.custid, outr.orderid, outr.orderdate, outr.empid
from [Sales].Orders as outr
where outr.orderdate = 
	(
	select max(inn.orderdate) as dt
	from [Sales].Orders as inn
	where inn.custid = outr.custid
	)
order by outr.custid





-- 6
-- Write a query that returns customers
-- who placed orders in 2007 but not in 2008
-- Tables involved: TSQL2012 database, Customers and Orders tables

select c.custid, c.companyname
from [Sales].Customers as c
where exists
	(select o.custid 
	from [Sales].Orders as o
	where  o.custid = c.custid  and
	year(o.orderdate) = '2007')
and not exists
	(select o.custid 
	from [Sales].Orders as o
	where  o.custid = c.custid  and
	year(o.orderdate) = '2008')




-- 7 (Optional, Advanced)
-- Write a query that returns customers
-- who ordered product 12
-- Tables involved: TSQL2012 database,
-- Customers, Orders and OrderDetails tables

-- Desired output:
--custid      companyname

select c.custid, c.companyname
from [Sales].Customers as c
where c.custid in 
	(select distinct o.custid 
	from [Sales].Orders as o
	where o.orderid in 
		(select distinct od.orderid 
		from [Sales].OrderDetails as od
		where od.productid = 12))



-- 8 (Optional, Advanced)
-- Write a query that calculates a running total qty
-- for each customer and month using subqueries
-- Tables involved: TSQL2012 database, Sales.CustOrders view


select co.custid, co.ordermonth, co.qty,	
	(select sum(innCo.qty) 
	from [Sales].CustOrders as innCo 
	where innCo.custid = co.custid and innCo.ordermonth <= co.ordermonth
	) as runqty
from [Sales].CustOrders as co
order by co.custid

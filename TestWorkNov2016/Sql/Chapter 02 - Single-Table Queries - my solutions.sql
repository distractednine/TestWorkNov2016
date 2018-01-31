USE TSQL2012;
GO

-- 1 
-- Return orders placed on June 2007
-- Tables involved: TSQL2012 database, Sales.Orders table

select top 1000
  orderid 
 ,orderdate
 ,custid
 ,empid 
 from [Sales].Orders
 where month([orderdate]) = 6 and year([orderdate]) = 2007
 order by orderdate


-- 2 
-- Return orders placed on the last day of the month
-- Tables involved: Sales.Orders table

select top 1000
  orderid 
 ,orderdate
 ,custid
 ,empid 
 from [Sales].Orders
 where [orderdate] = eomonth([orderdate])
 order by orderdate



-- 3 
-- Return employees with last name containing the letter 'a' twice or more
-- Tables involved: HR.Employees table

select top 1000
 empid 
 ,firstname
 ,lastname
 from [HR].Employees
 where lastname like '%a%a%'


-- 4 
-- Return orders with total value(qty*unitprice) greater than 10000
-- sorted by total value
-- Tables involved: Sales.OrderDetails table

select top 1000
  orderid 
 , sum(qty * unitprice) as [total value]
 from [Sales].OrderDetails
 group by orderid 
 having sum(qty*unitprice) > 10000
 order by sum(qty * unitprice) desc



-- 5 
-- Return the three ship countries with the highest average freight in 2007
-- Tables involved: Sales.Orders table

select top 3
  [shipcountry]
  , AVG([freight]) as [avgfreight]
 from [Sales].Orders
 where year([shippeddate]) = 2007
 --where [shippeddate] >= '20070101' and [shippeddate] <= '20080101'
 group by [shipcountry]
 order by AVG([freight]) desc



-- 6 
-- Calculate row numbers for orders
-- based on order date ordering (using order id as tiebreaker)
-- for each customer separately
-- Tables involved: Sales.Orders table

select top 1000
	custid      
	,orderdate
	,orderid
	, ROW_NUMBER() over (partition by custid order by orderdate, orderid) as rownum
	from [Sales].Orders
	order by custid    



-- 7
-- Figure out and return for each employee the gender based on the title of courtesy
-- Ms., Mrs. - Female, Mr. - Male, Dr. - Unknown
-- Tables involved: HR.Employees table


select top 1000 
empid       
,firstname  
,lastname             
,titleofcourtesy           
,	case titleofcourtesy
		when 'Ms.' then 'Female'
		when 'Mrs.' then 'Female'
		when 'Mr.' then 'Male'
		when 'Dr.' then 'Unknown'
		else 'Unknown'
	end
 as gender
from HR.Employees


-- 8 (advanced, optional)
-- Return for each customer the customer ID and region
-- sort the rows in the output by region
-- having NULLs sort last (after non-NULL values)
-- Note that the default in T-SQL is that NULL sort first
-- Tables involved: Sales.Customers table

select top 1000 
custid
,region
from Sales.Customers
order by (
	case  
		when region is null then 0
		else 1
	end
), region
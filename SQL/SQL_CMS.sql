---TASK1
create Database CourierMgmtSys
use CourierMgmtSys

--CREATING TABLES
create table Users 
(
UserID int identity(1,1) primary key not null,
Names varchar(255) not null,
Email varchar(255) unique not null,
Passwords varchar(255) not null,
ContactNumber varchar(20) unique not null,
UserAddress text
)

create table Courier(
CourierID int identity(975324680,1) primary key not null,
SenderName varchar(255) not null,
SenderAddress text not null,
ReceiverName varchar(255) not null,
ReceiverAddress text not null,
Weights decimal(5,2) not null,
CourierStatus varchar(50) not null,
TrackingNumber varchar(20) unique not null,
DeliveryDate date not null,
ServiceID int,
EmployeeID int,
LocationID int,
foreign key (ServiceID) references CourierServices(ServiceID),
foreign key (EmployeeID) references Employee(EmployeeID),
foreign key (LocationID) references Locations(LocationID)
)

create table CourierServices(
ServiceID int identity(192837,1) primary key not null,
ServiceName varchar(100) not null,
Cost decimal(8,2) not null,
)

create table Locations(
LocationID int identity(6300230,1) primary key not null,
LocationName varchar(100) not null,
Address text not null
)

create table Employee(
EmployeeID int identity(2025020,1) primary key not null,
Names varchar(255) not null,
Email varchar(255) unique not null,
ContactNumber varchar(20) not null,
Roles varchar(50) not null,
Salary decimal(10,2) not null,
LocationID int not null,
foreign key (LocationID) references Locations(LocationID)
)

create table Payment(
PaymentID int identity(101010,1) primary key not null,  
CourierID int not null,  
LocationId int not null,  
Amount decimal(10, 2) not null,  
PaymentDate date not null,  
foreign key (CourierID) references Courier(CourierID),  
foreign key (LocationID) references Locations(LocationID)
)


--INSERTING VALUES INTO USERS TABLE
insert into Users (Names, Email, Passwords, ContactNumber, UserAddress)
values('Priyanka Sharma', 'priyanka.sharma@email.com', 'P@ssw0rd123', '9876543210', 'Bangalore, India'),
('Rahul Verma', 'rahul.verma@email.com', 'SecureP@ss01', '9876543211', 'Delhi, India'),
('Ananya Rao', 'ananya.rao@email.com', 'Ananya@Pass', '9876543212', 'Mumbai, India'),
('Karthik Reddy', 'karthik.reddy@email.com', 'Karthik@99', '9876543213', 'Hyderabad, India'),
('Simran Kaur', 'simran.kaur@email.com', 'Simran$987', '9876543214', 'Chandigarh, India'),
('Vikram Singh', 'vikram.singh@email.com', 'VikramPass##', '9876543215', 'Pune, India'),
('Neha Gupta', 'neha.gupta@email.com', 'Neha@456', '9876543216', 'Kolkata, India'),
('Amit Tiwari', 'amit.tiwari@email.com', 'AmitPass987', '9876543217', 'Lucknow, India'),
('Pooja Desai', 'pooja.desai@email.com', 'Pooja@123', '9876543218', 'Surat, India'),
('Ravi Kumar', 'ravi.kumar@email.com', 'RaviPass@55', '9876543219', 'Chennai, India')


--INSERTING VALUES INTO COURIER TABLE
insert into Courier (SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weights, CourierStatus, TrackingNumber, DeliveryDate) 
values('Priyanka Sharma', 'Bangalore', 'Rahul Verma', 'Delhi', 2.50, 'In Transit', 'TRK975324680', '2025-03-27'),
('Rahul Verma', 'Delhi', 'Ananya Rao', 'Mumbai', 3.00, 'Delivered', 'TRK975324681', '2025-03-23'),
('Ananya Rao', 'Mumbai', 'Karthik Reddy', 'Hyderabad', 1.75, 'Shipped', 'TRK975324682', '2025-03-24'),
('Karthik Reddy', 'Hyderabad', 'Simran Kaur', 'Chandigarh', 2.10, 'Pending', 'TRK975324683', '2025-03-26'),
('Simran Kaur', 'Chandigarh', 'Vikram Singh', 'Pune', 3.25, 'Delivered', 'TRK975324684', '2025-03-14'),
('Vikram Singh', 'Pune', 'Neha Gupta', 'Kolkata', 2.00, 'In Transit', 'TRK975324685', '2025-03-30'),
('Neha Gupta', 'Kolkata', 'Amit Tiwari', 'Lucknow', 1.80, 'Shipped', 'TRK975324686', '2025-03-25'),
('Amit Tiwari', 'Lucknow', 'Pooja Desai', 'Surat', 2.45, 'Pending', 'TRK975324687', '2025-03-30'),
('Pooja Desai', 'Surat', 'Ravi Kumar', 'Chennai', 2.90, 'Delivered', 'TRK975324688', '2025-03-19'),
('Ravi Kumar', 'Chennai', 'Rhea Kapoor', 'Jaipur', 3.10, 'Shipped', 'TRK975324689', '2025-03-22')


--INSERTING VALUES INTO COURIERSERVICE TABLE
insert into CourierServices (ServiceName, Cost)
values('Express Delivery',150.00),
('Standard Delivery', 100.00),
('Same-Day Delivery', 200.00),
('Express Delivery', 150.00),
('Standard Delivery', 100.00),
('Express Delivery', 150.00),
('Overnight Delivery', 250.00),
('Standard Delivery', 100.00),
('Express Delivery', 150.00),
('Same-Day Delivery', 200.00)


--INSERTING VALUES INTO LOCATION TABLE
insert into Locations (LocationName, Address) 
values('Bangalore Hub', 'Bangalore, Karnataka, India'),
('Delhi Hub', 'New Delhi, Delhi, India'),
('Mumbai Hub', 'Mumbai, Maharashtra, India'),
('Hyderabad Hub', 'Hyderabad, Telangana, India'),
('Pune Hub', 'Pune, Maharashtra, India'),
('Kolkata Hub', 'Kolkata, West Bengal, India'),
('Chennai Hub', 'Chennai, Tamil Nadu, India'),
('Ahmedabad Hub', 'Ahmedabad, Gujarat, India'),
('Jaipur Hub', 'Jaipur, Rajasthan, India'),
('Lucknow Hub', 'Lucknow, Uttar Pradesh, India')


--INSERTING VALUES INTO EMPLOYEE TABLE
insert into Employee (Names, Email, ContactNumber, Roles, Salary, LocationID) 
values('Manoj Kumar', 'manoj.kumar@email.com', '9988776601', 'Manager', 75000.00, 6300230),
('Suresh Raina', 'suresh.raina@email.com', '9988776602', 'Courier Staff', 45000.00, 6300231),
('Alok Sharma', 'alok.sharma@email.com', '9988776603', 'Delivery Boy', 30000.00, 6300232),
('Priya Menon', 'priya.menon@email.com', '9988776604', 'Supervisor', 60000.00, 6300233),
('Vikas Singh', 'vikas.singh@email.com', '9988776605', 'Clerk', 35000.00, 6300234),
('Anjali Desai', 'anjali.desai@email.com', '9988776606', 'Manager', 78000.00, 6300230),
('Rohan Mehta', 'rohan.mehta@email.com', '9988776607', 'Courier Staff', 46000.00, 6300231),
('Neha Patel', 'neha.patel@email.com', '9988776608', 'Delivery Boy', 31000.00, 6300232),
('Amitabh Jha', 'amitabh.jha@email.com', '9988776609', 'Supervisor', 62000.00, 6300233),
('Meera Iyer', 'meera.iyer@email.com', '9988776610', 'Clerk', 37000.00, 6300234)


--INSERTING VALUES INTO PAYMENT TABLE
insert into Payment (CourierID, LocationId, Amount, PaymentDate) 
values(975324680, 6300230, 150.00, '2025-03-20'),
(975324681, 6300231, 100.00, '2025-03-15'),
(975324682, 6300232, 200.00, '2025-03-18'),
(975324683, 6300233, 150.00, '2025-03-22'),
(975324684, 6300234, 100.00, '2025-03-10'),
(975324685, 6300230, 180.00, '2025-03-21'),
(975324686, 6300231, 300.00, '2025-03-19'),
(975324687, 6300232, 100.00, '2025-03-23'),
(975324688, 6300233, 150.00, '2025-03-14'),
(975324689, 6300234, 250.00, '2025-03-17')

select * from Users
select * from Courier
select * from CourierServices
select * from Locations 
select * from Employee
select * from Payment


---TASK2
--1. LIST OF ALL CUSTOMERS
select *from Users


--2. LIST OF ALL ORDERS FOR A SPECIFIC CUSTOMER:
--IN DIFFERENT TABLE
select * from Courier where CourierID = 975324680
select * from Courier where CourierID = 975324687
select * from Courier where CourierID = 975324689
--WITHIN SAME TABLE
select * from Courier 
where CourierID in (975324680,975324687,975324689)

--3. LIST OF ALL COURIERS
select * from Courier

--4. LIST OF ALL PACKAGES FOR SPECIFIC ORDER
--COURIERID=975324680
select * from Courier where CourierID = 975324680
select * from Payment where CourierID = 975324680

--5. LIST OF ALL DELIVERIES FOR A SPECIFIC ORDER
select * from Courier where TrackingNumber = 'TRK975324681'
select * from Courier where TrackingNumber = 'TRK975324687'

--6. LIST OF ALL UNDELIVERED PACKAGES
select * from Courier where CourierStatus='Pending'

--7. LIST OF ALL PACKAGES THAT ARE SCHEDULED FOR DELIVERY TODAY
select * from Courier where CourierStatus='Delivered'

--8. LIST OF ALL PACKAGES WITH A SPECIFIC STATUS
select * from Courier where CourierStatus='In Transit'
select * from Courier where CourierStatus='Shipped'

--9. CALCULATE THE TOTAL NUMBER OF PACKAGES FOR EACH COURIER
select CourierID, count(*) as TotalPackages
from Courier
group by CourierID

--10. FIND THE AVERAGE DELIVERY TIME FOR EACH COURIER
select TrackingNumber, 
avg(datediff(day, PaymentDate, DeliveryDate)) as AvgDeliveryTime
from Courier C
join Payment P on C.CourierID = P.CourierID
group by TrackingNumber

--11. LIST OF ALL PACKAGES WITH A SPECIFIC WEIGHT RANGE
select * from Courier where Weights<=2.00
select * from Courier where Weights>=2.00
select * from Courier where Weights>=2.00 and Weights<=3.00

--12. RETRIEVE EMPLOYEE WHOSE NAMES CONTAIN 'NEHA PATEL'
select * from Employee where Names='Neha Patel'

--13. RETRIEVE ALL COURIER RECORDS WITH PAYMENTS GREATER THAN "200"
select * from Payment where Amount>=200
select * from Payment where Amount<=200


---TASK3
--14. FIND THE TOTAL NUMBER OF COURIERS HANDLED BY EACH EMPLOYEE
select LocationID, COUNT(CourierID) as TotalCouriersHandled
from Payment
group by LocationID

--15. CALCULATE THE TOTAL REVENUE GENERATED BY EACH LOCATION
select LocationID, SUM(Amount) as TotalRevenue
from Payment
group by LocationID

--16. FIND THE TOTAL NUMBER OF COURIERS DELIVERED TO EACH LOCATION
select
convert(varchar(255), ReceiverAddress) as Locations, 
count(CourierID) as TotalDelivered
from Courier
where CourierStatus = 'Delivered'
group by convert(varchar(255), ReceiverAddress)

--17. FIND THE COURIER WITH HIGHEST AVERAGE DELIVERY TIME
select TrackingNumber, 
avg(datediff(day, PaymentDate, DeliveryDate)) as AvgDeliveryTime
from Courier C
join Payment P on C.CourierID = P.CourierID
group by TrackingNumber
having avg(datediff(day, PaymentDate, DeliveryDate)) = (select max(AvgDelivery) 
from (select avg(datediff(day, PaymentDate, DeliveryDate)) as AvgDelivery
from Courier C
join Payment P on C.CourierID = P.CourierID
group by TrackingNumber) as SubQuery)

--18. FIND LOCATIONS WITH TOTAL PAYMENTS LESS THAN A CERTAIN AMOUNT
select LocationID, sum(Amount) as TotalPayments
from Payment
group by LocationID
having sum(Amount)<350
order by TotalPayments asc

--19. CALCULATE TOTAL PAYMENTS PER LOCATION
select LocationID, sum(Amount) as TotalPayments
from Payment
group by LocationID
order by TotalPayments desc

--20. RETRIEVE COURIERS WHO HAVE RECEIVED PAYMENTS TOTALING MORE THAN 100 IN A SPECIFIC LOCATION
select CourierID, SUM(Amount) AS TotalPayment
from Payment
where LocationID = 6300230
group by CourierID
having sum(Amount) > 100

--21. RETRIEVE COURIERS WHO HAVE RECEIVED PAYMENTS TOTALING MORE THAN 150 AFTER A CERTAIN DATE (PAYMENTDATE>'YYYY-MM-DD')
select CourierID, sum(Amount) as TotalPayment
from Payment
where PaymentDate > '2025-03-15'
group by CourierID
having sum(Amount) > 150

--22. RETRIEVE LOCATIONS WHERE THE TOTAL AMOUNT RECEIVED IS MORE THAN 200 BEFORE A CERTAIN DATE (PAYMENTDATE>'YYYY-MM-DD')
select LocationID, sum(Amount) as TotalAmountReceived
from Payment
where PaymentDate < '2025-03-20'
group by LocationID
having sum(Amount) > 200


---TASK4
--23. RETRIEVE PAYMENTS WITH COURIER INFORMATION
select * from Payment as P
inner join Courier as C
on P.CourierID = C.CourierID

--24. RETRIEVE PAYMENTS WITH LOCATION INFORMATION
select * from Payment as P
inner join Locations as L
on P.LocationId = L.LocationID

--25. RETRIEVE PAYMENTS WITH COURIER AND LOCATION INFORMATION
select * from Payment as P
inner join Courier as C
on P.CourierID = C.CourierID
inner join Locations as L
on P.LocationId = L.LocationID

--26. LIST ALL PAYMENTS WITH COURIER DETAILS
select * from Payment as P
left join Courier as C 
on P.CourierID = C.CourierID

--27. TOTAL PAYMENTS RECEIVED FOR EACH COURIER
select C.CourierID, C.SenderName, C.ReceiverName, sum(P.Amount) as TotalPayments
from Payment P
right join Courier C on P.CourierID = C.CourierID
group by C.CourierID, C.SenderName, C.ReceiverName

--28. LIST OF PAYMENTS MADE ON A SPECIFIC DATE
select * from Payment where PaymentDate = '2025-03-10'
select * from Payment where PaymentDate = '2025-03-19'

--29. GET COURIER INFORMATION FOR EACH PAYMENT 
select P.PaymentID, P.Amount, P.PaymentDate, 
C.CourierID, C.Weights, C.CourierStatus, C.TrackingNumber, C.DeliveryDate
from Payment as P
inner join Courier as C 
on P.CourierID = C.CourierID

--30. GET PAYMENT DETAILS WITH LOCATION
select * from Payment as P
inner join Locations as L
on P.LocationId=L.LocationID

--31. CALCULATING TOTAL PAYMENTS FOR EACH COURIER
select C.CourierID, sum(P.Amount) as TotalPayments  
from Payment as P
inner join Courier as C
on P.CourierID=C.CourierID
group by C.CourierID

--32. LIST PAYMENTS WITHIN A DATE RANGE
select CourierID, PaymentID, PaymentDate  
from Payment  
where PaymentDate between '2025-03-10' and '2025-03-20'  
order by PaymentDate

--33. RETRIEVE A LIST OF ALL USERS AND THEIR CORRESPONDING COURIER RECORDS, INCLUDING CASES WHERE THERE ARE NO MATCHES ON EITHER SIDE
select U.*, C.*
from Users U
full outer join Courier C 
on U.Names = C.SenderName OR U.Names = C.ReceiverName;

--34. RETRIEVE A LIST OF ALL COURIERS AND THEIR CORRESPONDING COURIER RECORDS, INCLUDING CASES WHERE THERE ARE NO MATCHES ON EITHER SIDE
select C.*, CS.*
from Courier C
full outer join CourierServices CS 
on C.ServiceID = CS.ServiceID;

--35. RETRIEVE A LIST OF ALL EMPLOYEES AND THEIR CORRESPONDING COURIER RECORDS, INCLUDING CASES WHERE THERE ARE NO MATCHES ON EITHER SIDE
select E.*, P.PaymentID, P.Amount, P.PaymentDate 
from Employee E
full outer join Payment P on E.LocationID = P.LocationID

--36. LIST ALL USERS AND ALL COURIER SERVICES, SHOWING ALL POSSIBLE COMBINATIONS
select * from Users as U
cross join CourierServices as CS

--37. LIST ALL EMPLOYEES AND ALL LOCATIONS, SHOWING ALL POSSIBLE COMBINATIONS
select * from Employee as E
cross join Locations as L

--38. RETRIEVE A LIST OF COURIER AND THEIR CORRESPONDING SENDER INFORMATION(IF AVAILABLE)
select C.CourierID, C.SenderName,C.SenderAddress, U.UserID, U.Email
from Courier C
left join Users U on C.SenderName = U.Names;

--39. RETRIEVE A LIST OF COURIER AND THEIR CORRESPONDING RECEIVER INFORMATION(IF AVAILABLE)
select C.CourierID, C.ReceiverName, C.ReceiverAddress, U.UserID,U.Email
from Courier C
left join Users U on C.ReceiverName = U.Names;

--40. RETRIEVE A LIST OF COURIERS ALONG WITH THE COURIER SERVICE DETAILS(IF AVAILABLE)
select C.*, CS.*
from Courier C
left join CourierServices CS on C.ServiceID = CS.ServiceID;

--41. RETRIEVE A LIST OF EMPLOYEES AND THE NUMBER OF COURIERS ASSIGNED TO EACH EMPLOYEE
select E.EmployeeID, E.Names, count(P.CourierID) as TotalCouriersAssigned
from Employee E
join Payment P on E.LocationID = P.LocationID
group by E.EmployeeID, E.Names

--42. RETRIEVE A LIST OF LOCATIONS AND THE TOTAL PAYMENT AMOUNT RECEIVED AT EACH LOCATION
select L.LocationID, L.LocationName, sum(P.Amount) as TotalPayments
from Locations L
inner join Payment P on L.LocationID = P.LocationID
group by L.LocationID, L.LocationName

--43. RETRIEVE ALL COURIERS SENT BY THE SAME SENDER(BASED ON SENDER)
if exists (select 1 from Courier group by SenderName having count(*) > 1)
select C.*
from Courier C
where C.SenderName IN (select SenderName from Courier group by SenderName having count(*) > 1)
else
print 'No couriers by same sender'

--44. LIST ALL EMPLOYEES WHO SHARE SAME ROLE
select E.EmployeeID, E.Roles 
from Employee as E
where E.Roles in ('Manager','Courier Staff','Delivery boy','Clerk','Supervisor')
order by E.Roles

--45. RETRIEVE ALL PAYMENTS MADE FOR COURIERS SENT FROM THE SAME LOCATION
select P.PaymentID, P.Amount, P.LocationId, L.LocationName
from Payment as P
join Locations as L
on P.LocationId=L.LocationId

--46. RETRIEVE ALL COURIERS SENT FROM THE SAME LOCATION.
if exists (
    select 1 
    from Courier C1
    inner join Courier C2 
    on cast(C1.SenderAddress as varchar(max)) = cast(C2.SenderAddress as varchar(max))
    where C1.CourierID <> C2.CourierID
)
    select C1.*
    from Courier C1
    inner join Courier C2 
    on cast(C1.SenderAddress as varchar(max)) = cast(C2.SenderAddress as varchar(max))
    where C1.CourierID <> C2.CourierID;
else
    print 'No couriers sent from the same location'

--47. LIST EMPLOYEES AND THE NUMBER OF COURIERS THEY HAVE DELIVERED
select E.EmployeeID, E.Names, count(C.CourierID) as DeliveredCouriers
from Employee E
inner join Locations L on E.LocationID = L.LocationID
inner join Payment P on L.LocationID = P.LocationID
inner join Courier C on P.CourierID = C.CourierID
where C.CourierStatus = 'Delivered'
group by E.EmployeeID, E.Names

--48. FIND COURIERS THAT WERE PAID AN AMOUNT GREATER THAN THE COST OF THEIR RESPECTIVE COURIER SERVICES 
select C.CourierID, C.SenderName, C.ReceiverName, P.Amount as PaymentAmount, CS.Cost as ServiceCost
from Courier C
join Payment P on C.CourierID = P.CourierID
join CourierServices CS on C.ServiceID = CS.ServiceID
where P.Amount > CS.Cost;

---TASK5
--49. FIND COURIERS THAT HAVE A WEIGHT GREATER THAN THE AVERAGE WEIGHT OF ALL COURIERS
select C.CourierID, C.SenderName, C.Weights from Courier as C
where Weights > (select avg(Weights) from Courier)

--50. FIND THE NAMES OF ALL EMPLOYEES WHO HAVE A SALARY GREATER THAN THE AVERAGE SALARY
select E.EmployeeID, E.Names, E.Salary from Employee as E
where Salary > (select avg(Salary) from Employee)

--51. FIND THE TOTAL COST OF ALL COURIER SERVICES WHERE THE COST IS LESS THAN THE MAXIMUM COST
select sum(Cost) as TotalCost from CourierServices
where Cost<(select max(Cost) from CourierServices)

--52. FIND ALL COURIERS THAT HAVE BEEN PAID FOR
select C.CourierID, C.SenderName, C.CourierStatus, C.TrackingNumber 
from Courier as c
where exists (select * from Payment as P where C.CourierID = P.CourierID)

--53. FIND THE LOCATION WHERE THE MAXIMUM AMOUNT WAS MADE
select P.LocationId, P.Amount from Payment as P
where Amount = (select max(Amount) from Payment as P)

--54. FIND ALL COURIERS WHOSE WEIGHT IS GREATER THAN THE WEIGHT OF ALL COURIERS SENT BY A SPECIFIC SENDER
select CourierID, Weights
from Courier
where Weights = (select max(Weights) from Courier)

--ADDITIONAL MODIFICATIONS 
alter table Employee add Passwords varchar(255)
update Employee set Passwords = 'Manoj@2025' WHERE EmployeeID = 2025020;
update Employee set Passwords = 'Suresh@2025' WHERE EmployeeID = 2025021;
update Employee set Passwords = 'Alok@2025' WHERE EmployeeID = 2025022;
update Employee set Passwords = 'Priya@2025' WHERE EmployeeID = 2025023;
update Employee set Passwords = 'Vikas@2025' WHERE EmployeeID = 2025024;
update Employee set Passwords = 'Anjali@2025' WHERE EmployeeID = 2025025;
update Employee set Passwords = 'Rohan@2025' WHERE EmployeeID = 2025026;
update Employee set Passwords = 'Neha@2025' WHERE EmployeeID = 2025027;
update Employee set Passwords = 'Amitabh@2025' WHERE EmployeeID = 2025028;
update Employee set Passwords = 'Meera@2025' WHERE EmployeeID = 2025029;
select * from Employee

update Courier set EmployeeID = '2025022' where CourierID = 975324680;
update Courier set EmployeeID = '2025022' where CourierID = 975324681;
update Courier set EmployeeID = '2025027' where CourierID = 975324682;
update Courier set EmployeeID = '2025027' where CourierID = 975324683;
update Courier set EmployeeID = '2025027' where CourierID = 975324684;
update Courier set EmployeeID = '2025022' where CourierID = 975324685;
update Courier set EmployeeID = '2025022' where CourierID = 975324686;
update Courier set EmployeeID = '2025027' where CourierID = 975324687;
update Courier set EmployeeID = '2025022' where CourierID = 975324688;
update Courier set EmployeeID = '2025027' where CourierID = 975324689;
select * from Courier


create table TrackingHistory (
HistoryID int identity(101,1) primary key,
TrackingNumber varchar(20),
LocationUpdate varchar(255),
CourierStatus varchar(50),
UpdatedTime datetime default getdate(),
foreign key (TrackingNumber) references Courier(TrackingNumber)
)

insert into TrackingHistory (TrackingNumber, LocationUpdate, CourierStatus)
values('TRK975324680', 'Reached Bangalore Hub', 'In Transit'),
('TRK975324681', 'Delivered to receiver in Mumbai', 'Delivered'),
('TRK975324682', 'Departed from Mumbai Facility', 'Shipped'),
('TRK975324683', 'Awaiting Pickup in Hyderabad', 'Pending'),
('TRK975324684', 'Delivered at Pune Address', 'Delivered'),
('TRK975324685', 'Left Pune Center, En route to Kolkata', 'In Transit'),
('TRK975324686', 'Arrived at Kolkata Main Facility', 'Shipped'),
('TRK975324687', 'Shipment created in Lucknow', 'Pending'),
('TRK975324688', 'Out for Delivery in Chennai', 'Delivered'),
('TRK975324689', 'Left Chennai, heading to Jaipur', 'Shipped')
select * from TrackingHistory

alter table Users add ZipCode int
update Users set UserAddress = '123 M G Road, Bangalore, Karnataka', ZipCode = '560001' where UserID = 1;
update Users set UserAddress = '45 Connaught Place, Delhi, Delhi', ZipCode = '110001' where UserID = 2;
update Users set UserAddress = '78 Marine Drive, Mumbai, Maharashtra', ZipCode = '400001' where UserID = 3;
update Users set UserAddress = '89 Banjara Hills, Hyderabad, Telangana', ZipCode = '500034' where UserID = 4;
update Users set UserAddress = '67 Sector 17, Chandigarh, Punjab', ZipCode = '160017' where UserID = 5;
update Users set UserAddress = '34 FC Road, Pune, Maharashtra', ZipCode = '411004' where UserID = 6;
update Users set UserAddress = '22 Park Street, Kolkata, West Bengal', ZipCode = '700016' where UserID = 7;
update Users set UserAddress = '11 Hazratganj, Lucknow, Uttar Pradesh', ZipCode = '226001' where UserID = 8;
update Users set UserAddress = '56 Ring Road, Surat, Gujarat', ZipCode = '395002' where UserID = 9;
update Users set UserAddress = '88 T Nagar, Chennai, Tamil Nadu', ZipCode = '600017' where UserID = 10;
select * from Users

update Courier set LocationID = '6300230' where CourierID = 975324680;
update Courier set LocationID = '6300231' where CourierID = 975324681;
update Courier set LocationID = '6300232' where CourierID = 975324682;
update Courier set LocationID = '6300233' where CourierID = 975324683;
update Courier set LocationID = '6300234' where CourierID = 975324684;
update Courier set LocationID = '6300235' where CourierID = 975324685;
update Courier set LocationID = '6300236' where CourierID = 975324686;
update Courier set LocationID = '6300237' where CourierID = 975324687;
update Courier set LocationID = '6300238' where CourierID = 975324688;
update Courier set LocationID = '6300239' where CourierID = 975324689;

update Courier set ServiceID = '192837' where CourierID = 975324680;
update Courier set ServiceID = '192838' where CourierID = 975324681;
update Courier set ServiceID = '192839' where CourierID = 975324682;
update Courier set ServiceID = '192840' where CourierID = 975324683;
update Courier set ServiceID = '192841' where CourierID = 975324684;
update Courier set ServiceID = '192842' where CourierID = 975324685;
update Courier set ServiceID = '192843' where CourierID = 975324686;
update Courier set ServiceID = '192844' where CourierID = 975324687;
update Courier set ServiceID = '192845' where CourierID = 975324688;
update Courier set ServiceID = '192846' where CourierID = 975324689;
select * from Courier
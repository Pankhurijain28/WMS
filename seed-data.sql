SET NOCOUNT ON;

/* ===== Departments ===== */
IF NOT EXISTS (SELECT 1 FROM Departments)
BEGIN
    INSERT INTO Departments (DepartmentName, Description, CreatedOn) VALUES
        ('Engineering',     'Software design and development',        GETDATE()),
        ('Human Resources', 'Recruitment, payroll and employee care', GETDATE()),
        ('Finance',         'Accounting, budgeting and audits',       GETDATE()),
        ('Sales',           'Client acquisition and revenue',         GETDATE()),
        ('Marketing',       'Branding, campaigns and outreach',       GETDATE()),
        ('IT Support',      'Infrastructure and helpdesk',            GETDATE());
END

/* ===== Employees ===== (RoleId 1=Admin 2=Manager 3=Employee) */
IF NOT EXISTS (SELECT 1 FROM Employees)
BEGIN
    INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, Gender, DOB, DOJ, DepartmentId, RoleId, Status, CreatedOn)
    SELECT v.FirstName, v.LastName, v.Email, v.PhoneNumber, v.Gender, v.DOB, v.DOJ,
           d.DepartmentId, v.RoleId, 'Active', GETDATE()
    FROM (VALUES
        ('Aarav','Sharma','aarav.sharma@wms.com','9876543201','M','1989-03-12','2018-06-01','Engineering',2),
        ('Diya','Patel','diya.patel@wms.com','9876543202','F','1990-07-22','2019-02-15','Human Resources',2),
        ('Rohan','Mehta','rohan.mehta@wms.com','9876543203','M','1994-11-05','2020-08-10','Engineering',3),
        ('Ananya','Iyer','ananya.iyer@wms.com','9876543204','F','1996-01-18','2021-03-01','Finance',3),
        ('Vivaan','Reddy','vivaan.reddy@wms.com','9876543205','M','1993-09-30','2019-11-20','Sales',3),
        ('Ishita','Nair','ishita.nair@wms.com','9876543206','F','1997-05-14','2022-01-05','Marketing',3),
        ('Aditya','Kumar','aditya.kumar@wms.com','9876543207','M','1992-12-02','2020-05-18','IT Support',3),
        ('Saanvi','Gupta','saanvi.gupta@wms.com','9876543208','F','1995-08-25','2021-09-12','Engineering',3),
        ('Arjun','Singh','arjun.singh@wms.com','9876543209','M','1991-04-09','2018-12-03','Finance',3),
        ('Kavya','Joshi','kavya.joshi@wms.com','9876543210','F','1998-02-28','2023-04-22','Sales',3),
        ('Kabir','Rao','kabir.rao@wms.com','9876543211','M','1996-10-16','2022-07-30','Marketing',3),
        ('Myra','Desai','myra.desai@wms.com','9876543212','F','1999-06-21','2023-10-09','IT Support',3)
    ) AS v(FirstName, LastName, Email, PhoneNumber, Gender, DOB, DOJ, DeptName, RoleId)
    JOIN Departments d ON d.DepartmentName = v.DeptName;
END

/* ===== Clients ===== */
IF NOT EXISTS (SELECT 1 FROM Clients)
BEGIN
    INSERT INTO Clients (ClientName, ClientAddress, ClientPhoneNumber, ClientLocation, Status) VALUES
        ('Acme Corporation',  '123 Business Park, Andheri',   '9988776655', 'Mumbai',    1),
        ('Globex Ltd',        '45 Tech Avenue, Whitefield',   '9876501234', 'Bangalore', 1),
        ('Initech Solutions', '78 Cyber City, Madhapur',      '9123456780', 'Hyderabad', 1),
        ('Umbrella Inc',      '12 Marine Drive, Koregaon',    '9012345678', 'Pune',      1);
END

/* ===== Projects ===== */
IF NOT EXISTS (SELECT 1 FROM Projects)
BEGIN
    INSERT INTO Projects (ProjectName, ClientId, StartDate, EndDate, Status)
    SELECT v.ProjectName, c.ClientId, v.StartDate, v.EndDate, v.Status
    FROM (VALUES
        ('Payroll Revamp',        'Acme Corporation',  '2025-01-15', NULL,         'Active'),
        ('E-Commerce Platform',   'Globex Ltd',        '2025-03-01', NULL,         'Active'),
        ('Mobile Banking App',    'Initech Solutions', '2025-05-20', NULL,         'Active'),
        ('CRM Migration',         'Umbrella Inc',      '2024-08-10', '2025-04-30', 'Completed'),
        ('HR Analytics Dashboard','Acme Corporation',  '2025-06-01', NULL,         'Active')
    ) AS v(ProjectName, ClientName, StartDate, EndDate, Status)
    JOIN Clients c ON c.ClientName = v.ClientName;
END

/* ===== Employee Project Allocations ===== */
IF NOT EXISTS (SELECT 1 FROM EmployeeProjectAllocations)
BEGIN
    INSERT INTO EmployeeProjectAllocations (EmpId, ProjectId, AssignedOn, CreateDate, CreatedBy, Status)
    SELECT e.EmployeeId, p.ProjectId, CAST(GETDATE() AS DATE), CAST(GETDATE() AS DATE), 'admin', 1
    FROM (VALUES
        ('aarav.sharma@wms.com','Payroll Revamp'),
        ('rohan.mehta@wms.com','Payroll Revamp'),
        ('saanvi.gupta@wms.com','E-Commerce Platform'),
        ('rohan.mehta@wms.com','E-Commerce Platform'),
        ('aditya.kumar@wms.com','Mobile Banking App'),
        ('ananya.iyer@wms.com','CRM Migration'),
        ('vivaan.reddy@wms.com','HR Analytics Dashboard'),
        ('myra.desai@wms.com','Mobile Banking App')
    ) AS v(Email, ProjectName)
    JOIN Employees e ON e.Email = v.Email
    JOIN Projects  p ON p.ProjectName = v.ProjectName;
END

/* ===== Attendance (last 5 days for 4 employees) ===== */
IF NOT EXISTS (SELECT 1 FROM Attendances)
BEGIN
    ;WITH Days AS (
        SELECT 0 AS n UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4
    ),
    Emps AS (
        SELECT EmployeeId FROM Employees
        WHERE Email IN ('rohan.mehta@wms.com','ananya.iyer@wms.com','vivaan.reddy@wms.com','saanvi.gupta@wms.com')
    )
    INSERT INTO Attendances (EmpId, CheckIn, CheckOut, TotalHours, WorkMode, AttendanceDate)
    SELECT e.EmployeeId,
           DATEADD(HOUR, 9,  CAST(CAST(DATEADD(DAY, -d.n, GETDATE()) AS DATE) AS datetime2)),
           DATEADD(HOUR, 18, CAST(CAST(DATEADD(DAY, -d.n, GETDATE()) AS DATE) AS datetime2)),
           9.0,
           CASE WHEN d.n % 2 = 0 THEN 'WFO' ELSE 'WFH' END,
           CAST(DATEADD(DAY, -d.n, GETDATE()) AS DATE)
    FROM Emps e CROSS JOIN Days d;
END

/* ===== Leaves (mixed statuses) ===== */
IF NOT EXISTS (SELECT 1 FROM Leaves)
BEGIN
    INSERT INTO Leaves (EmpId, LeaveType, Reason, FromDate, ToDate, Status, AppliedOn, ApprovedBy, ApprovedOn)
    SELECT e.EmployeeId, v.LeaveType, v.Reason, v.FromDate, v.ToDate, v.Status,
           v.AppliedOn, v.ApprovedBy, v.ApprovedOn
    FROM (VALUES
        ('rohan.mehta@wms.com','Sick',  'Fever and rest',        '2026-06-10','2026-06-11','Approved', '2026-06-08', 1, '2026-06-09'),
        ('ananya.iyer@wms.com','Casual','Family function',       '2026-06-20','2026-06-21','Pending',  '2026-06-15', NULL, NULL),
        ('vivaan.reddy@wms.com','Earned','Vacation trip',        '2026-07-01','2026-07-05','Pending',  '2026-06-16', NULL, NULL),
        ('saanvi.gupta@wms.com','Sick',  'Medical checkup',      '2026-06-05','2026-06-05','Approved', '2026-06-03', 1, '2026-06-04'),
        ('kavya.joshi@wms.com', 'Casual','Personal work',        '2026-06-12','2026-06-12','Rejected', '2026-06-10', 1, '2026-06-11'),
        ('aditya.kumar@wms.com','Earned','Wedding in family',    '2026-06-25','2026-06-28','Pending',  '2026-06-17', NULL, NULL)
    ) AS v(Email, LeaveType, Reason, FromDate, ToDate, Status, AppliedOn, ApprovedBy, ApprovedOn)
    JOIN Employees e ON e.Email = v.Email;
END

/* ===== Announcements ===== */
IF NOT EXISTS (SELECT 1 FROM Announcements)
BEGIN
    INSERT INTO Announcements (Title, Message, CreatedBy, CreatedOn, IsActive) VALUES
        ('Welcome to WMS',         'The new Workforce Management System is now live. Explore the dashboard!', 1, GETDATE(), 1),
        ('Holiday Notice',         'Office will remain closed on the upcoming national holiday.',             1, GETDATE(), 1),
        ('Quarterly Town Hall',    'All-hands meeting scheduled for the last Friday of this month at 4 PM.',  1, GETDATE(), 1);
END

SELECT
    (SELECT COUNT(*) FROM Departments) AS Departments,
    (SELECT COUNT(*) FROM Employees)   AS Employees,
    (SELECT COUNT(*) FROM Clients)     AS Clients,
    (SELECT COUNT(*) FROM Projects)    AS Projects,
    (SELECT COUNT(*) FROM EmployeeProjectAllocations) AS Allocations,
    (SELECT COUNT(*) FROM Attendances) AS Attendances,
    (SELECT COUNT(*) FROM Leaves)      AS Leaves,
    (SELECT COUNT(*) FROM Announcements) AS Announcements;

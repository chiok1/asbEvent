USE master;
GO

IF DB_ID('asb_event_dev') IS NULL
BEGIN
    CREATE DATABASE asb_event_dev;
    PRINT 'Database asb_event_dev created';
END;
GO

CREATE TABLE EventRegistration (
    EventId INT NOT NULL,
    EmployeeName NVARCHAR(255) NOT NULL,
    EmpID NVARCHAR(255) NOT NULL,
    Company NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    EmployeeEmail NVARCHAR(255) NOT NULL,
    Country NVARCHAR(255) NOT NULL,
    RegistrationDate DATETIME NOT NULL,
    PRIMARY KEY (EventId, EmpID) -- Assuming EventId and EmpID together uniquely identify a registration
);
GO


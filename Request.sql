
-- Create the Requests table
CREATE TABLE Requests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT GETDATE()
);

-- Insert some sample data
INSERT INTO Requests (Name, Status, Timestamp)
VALUES 
('Request A', 'Pending', GETDATE()),
('Request B', 'Pending', GETDATE()),
('Request C', 'Pending', GETDATE());


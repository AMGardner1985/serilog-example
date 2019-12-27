
CREATE PROCEDURE [dbo].[CreateNewCustomer]
	@Name NVARCHAR(MAX),
	@TotalPuchases MONEY,
	@TotalReturns MONEY
AS
	INSERT INTO dbo.Customers
	VALUES (@Name, @TotalPuchases, @TotalReturns)


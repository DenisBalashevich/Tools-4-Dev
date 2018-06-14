CREATE PROCEDURE [Northwind].[SaveTransactionSampleProcedure]
as
begin
	set nocount on
	declare @trancount int = @@trancount

	save tran SaveTransactionSampleProcedure1

	begin try
		if @trancount = 0
			begin tran
		else
			save tran SaveTransactionSampleProcedure

		---
		-- Some code
		---
		
		commit tran
	end try
	begin catch
		declare @xActState int = xact_state()
		declare @errorNumber int = error_number()
		declare @errorMessage nvarchar(max) = error_message()

		if (@xActState = -1) or (@xActState = 1 and @trancount = 0)
			rollback tran
		else if @xActState = 1 and @trancount > 0
			rollback tran SaveTransactionSampleProcedure
		
		raiserror(50010, 11, 0, N'SaveTransactionSampleProcedure', @errorNumber, @errorMessage)
		return
	end catch


end
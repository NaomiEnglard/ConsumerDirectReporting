USE [WSMProcessing]
GO

/****** Object:  StoredProcedure [dbo].[UpdateClientSCInfo]    Script Date: 1/25/2016 12:02:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










-- =============================================
-- Author:		E Katz
-- Create date: January 19, 2016
-- Description:	Updates Smart Credit information for a client
-- =============================================
ALTER PROCEDURE [dbo].[UpdateClientSCInfo] 
	
	@signupDate date,
	@memberId as varchar(50),
	@signupMethod as varchar(50),
	@frodoId as varchar(20),
	@email as varchar(50),
	@returnCode as int OUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @clientId varchar(50)
	IF EXISTS (SELECT ClientId from Clients where ClientId = @frodoId)
	BEGIN
		SET @clientId = @frodoId
		SET @returnCode = 1  -- Matched frodo id
	END
	ELSE
	BEGIN
		-- Try to match email
		SELECT @clientId = c.ClientId from 
			Clients c join Names n on n.NameId = c.PrimaryNameId
			WHERE n.email = @email

		if @clientId is null
		BEGIN
			set @returnCode = -1
			return
		END
		ELSE
		BEGIN
			SET @returnCode = 2  -- Matched email
		END

	END
	UPDATE Clients SET
		SmartCreditMemberId = @memberId,
		SmartCreditSignupDate = @signupDate,
		SmartCreditSignupMethod = @signupMethod
		WHERE ClientId = @clientId
	
	Return
	
END










GO



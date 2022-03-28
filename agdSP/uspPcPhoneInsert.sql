/****************************************************************
** Name: [agdSp].[uspPcPhoneInsert]
** Desc: 電腦電話新增
**
** Return values: 0 成功
** Return Recordset: 
**	NA
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneInsert] 
	 
	,@Creator = @Creator
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
	,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-28 14:45:46    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneInsert] (
	    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbPcPhone] (
			extCode
			,computerName
			,computerIP
			,memo
			,isEnable
			,creator
			,updator
			,createDT
			,updateDT
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@extCode
			,@computerName
			,@computerIP
			,@memo
			,@isEnable
			,@creator
			,@updator
			,@createDT
			,@updateDT
			,GETDATE()
			,@Creator
			,GETDATE()
			,@Creator
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
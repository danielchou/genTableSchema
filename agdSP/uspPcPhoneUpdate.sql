/****************************************************************
** Name: [agdSp].[uspPcPhoneUpdate]
** Desc: 電腦電話更新
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
    @SeqNo	INT - 流水號
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1

EXEC @return_value = [agdSp].[uspPcPhoneUpdate]
    @SeqNo = @SeqNo
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
CREATE PROCEDURE [agdSp].[uspPcPhoneUpdate] (
	@SeqNo INT
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbPcPhone
		SET extCode = @extCode
			,computerName = @computerName
			,computerIP = @computerIP
			,memo = @memo
			,isEnable = @isEnable
			,creator = @creator
			,updator = @updator
			,createDT = @createDT
			,updateDT = @updateDT
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
/****************************************************************
** Name: [agdSp].[usp{tb}Update]
** Desc: {tbDscr}更新
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
    {pt_input}
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,{pt_Declare}
    ,@ErrorMsg NVARCHAR(100)

    {pt_SetValue}

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,{pt_Exec}
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** {pt_DateTime}    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[usp{tb}Update] (
	{pt_Declare}
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tb{tb}
		SET {pt_UpdateSet}
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

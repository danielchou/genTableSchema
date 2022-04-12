/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_delete]
** Desc: $pt_tbDscr刪除
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
	$pt_inputPK
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
	DECLARE @return_value INT
		,$pt_DeclarePK
		,@ErrorMsg NVARCHAR(100)

	$pt_deleteSetVal

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_delete] 
		$pt_cmmtExecPK
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:		 	Author:				Description:
** ---------- ------- ------------------------------------
** $pt_DateTime 	Jerry Yang			first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_delete] (
	$pt_AtParasSetPK
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		DELETE agdSet.tb$pt_tableName
		WHERE $pt_queryWherePK;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
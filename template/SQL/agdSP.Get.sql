/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_get]
** Desc: $pt_tbDscr查詢
**
** Return values: 0 成功
** Return Recordset: 
$pt_getSelectAll
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	$pt_input
**
**   Output
** -----------
	@ErrorMsg 	NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
	DECLARE @return_value INT
	,$pt_DeclareGet
	,@ErrorMsg NVARCHAR(100)

	$pt_getSetValue

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_get]
		$pt_ExecGet
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** $pt_DateTime    	Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_get] (
		$pt_DeclarePK
		,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
			$pt_fColAll
		FROM $pt_schema.tb$pt_tableName AS f
		WHERE $pt_queryWhere
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_insert]
** Desc: $pt_tbDscr新增
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
	$pt_input
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value int
	,$pt_DeclareInsert
	,@ErrorMsg NVARCHAR(100);

	$pt_insertSetVal

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_insert] 
	$pt_ExecInsert
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
	,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** $pt_DateTime    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_insert] (
	$pt_DeclareInsert
	,@ErrorMsg 		NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [$pt_schema].[tb$pt_tableName] (
			$pt_insertCols
        )
		VALUES (
			$pt_insertVals
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
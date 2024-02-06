/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_exists]
** Desc: $pt_tbDscr查詢是否重複
**
** Return values: 0 成功
** Return Recordset: 
**	Total		:資料總筆數
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
    ,$pt_Declare
    ,@ErrorMsg NVARCHAR(100)

    $pt_existSetValue

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_exists] 
	$pt_Exec
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** $pt_DateTime    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_exists]
    $pt_Declare
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT TOP 1 1 AS Total
		FROM $pt_schema.tb$pt_tableName
		WHERE $pt_sqNoHardCode
			AND ( 
                $pt_fColOr
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
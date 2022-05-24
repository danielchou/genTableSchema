/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_update]
** Desc: $pt_tbDscr更新
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
	@Updater 		VARCHAR(11)		- 更新者
	@UpdaterName 	NVARCHAR(60)	- 更新人員
**
**   Output
** -----------
	@ErrorMsg 		NVARCHAR(100) 	- 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,$pt_Declare
	,@Updater VARCHAR(11)
	,@UpdaterName NVARCHAR(60)
    ,@ErrorMsg NVARCHAR(100)

    $pt_updateSetVal
	SET @Updater = 'admin'
	SET @UpdaterName = 'admin'

EXEC @return_value = [agdSp].[usp$pt_tableName$pt_update]
    $pt_Exec
	,@Updater = @Updater
	,@UpdaterName = @UpdaterName
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
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_update] (
	$pt_Declare
	,@Updater VARCHAR(11)
	,@UpdaterName NVARCHAR(60)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE $pt_schema.tb$pt_tableName
		SET $pt_UpdateSet
            ,UpdateDT = DATEADD(HH, +8, GETUTCDATE())
			,Updater = @Updater
			,UpdaterName = @UpdaterName
		WHERE $pt_sqNo = @$pt_sqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
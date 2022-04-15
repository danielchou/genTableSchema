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
	@Creator		 VARCHAR(20) - 建立者
	@CreatorName	 NVARCHAR(60) - 建立人員
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,$pt_Declare
	,@Creator VARCHAR(20)
	,@CreatorName NVARCHAR(60)
	,@ErrorMsg NVARCHAR(100);

	$pt_insertSetVal
	SET @Creator = 'admin'
	SET @CreatorName = 'admin'

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_insert] 
		$pt_Exec
		,@Creator = @Creator
		,@CreatorName = @CreatorName
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
	$pt_Declare
	,@Creator VARCHAR(20)
	,@CreatorName NVARCHAR(60)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tb$pt_tableName] (
			$pt_insertCols
			,CreateDT
			,Creator
			,CreatorName
			,UpdateDT
			,Updator
			,UpdatorName
        )
		VALUES (
			$pt_insertVals
			,DATEADD(HH, +8, GETUTCDATE())
			,@Creator
			,@CreatorName
			,DATEADD(HH, +8, GETUTCDATE())
			,@Creator
			,@CreatorName
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
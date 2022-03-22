/****************************************************************
** Name: [agdSp].[uspGroupInsert]
** Desc: 群組單位新增
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
	@GroupId VARCHAR(20)	-	部門代碼
	@GroupName NVARCHAR(50)	-	部門名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @GroupId = '1'
	SET @GroupName = 'admin'
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspGroupInsert] 
	 @GroupId = @GroupId
	,@GroupName = @GroupName
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
** 2022-03-22 23:44:28    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspGroupInsert] (
	@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
INSERT INTO [agdSet].[tbCode]
           (
			[GroupId]
			,[GroupName]
			,[CreateDT]
			,[Creator]
			,[UpdateDT]
			,[Updator]
        )
		VALUES (
			@GroupId
			,@GroupName
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

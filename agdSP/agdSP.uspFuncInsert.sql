/****************************************************************
** Name: [agdSp].[uspFuncInsert]
** Desc: 系統功能新增
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
	@FuncId VARCHAR(20)	-	功能代碼
	@FuncName NVARCHAR(50)	-	功能名稱
	@FuncPath NVARCHAR(30)	-	功能路由
	@FuncIcon NVARCHAR(30)	-	功能Icon
	@IsEnable BIT	-	是否啟用
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(30)
	,@FuncIcon NVARCHAR(30)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @FuncId = '3'
	SET @FuncName = '職務權限設定'
	SET @FuncPath = '/admin/auth/index'
	SET @FuncIcon = 'fm-icon-home'
	SET @IsEnable = '1'
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspFuncInsert] 
	 @FuncId = @FuncId
	,@FuncName = @FuncName
	,@FuncPath = @FuncPath
	,@FuncIcon = @FuncIcon
	,@IsEnable = @IsEnable
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
** 2022-03-22 23:44:27    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspFuncInsert] (
	@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(30)
	,@FuncIcon NVARCHAR(30)
	,@IsEnable BIT    
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
			[FuncId]
			,[FuncName]
			,[FuncPath]
			,[FuncIcon]
			,[IsEnable]
			,[CreateDT]
			,[Creator]
			,[UpdateDT]
			,[Updator]
        )
		VALUES (
			@FuncId
			,@FuncName
			,@FuncPath
			,@FuncIcon
			,@IsEnable
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

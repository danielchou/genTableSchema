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
	@FuncId	VARCHAR(20) - 功能ID
	@FuncName	NVARCHAR(50) - 功能名稱
	@FuncPath	NVARCHAR(20) - 功能路由
	@FuncIcon	NVARCHAR(20) - 功能圖示
	@IsEnable	BIT - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
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
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @FuncId = 'A0023'
	SET @FuncName = '職務權限設定'
	SET @FuncPath = '/admin/auth/index'
	SET @FuncIcon = 'fm-icon-home'
	SET @IsEnable = 1
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
** 2022-03-28 11:27:22    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncInsert] (
	@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbFunc] (
			FuncId
			,FuncName
			,FuncPath
			,FuncIcon
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
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
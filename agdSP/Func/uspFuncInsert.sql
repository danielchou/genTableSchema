/****************************************************************
** Name: [agdSp].[uspFuncInsert]
** Desc: 功能新增
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
	@FuncID          VARCHAR(20)  - 功能代碼
	@FuncName        NVARCHAR(50) - 功能名稱
	@ParentFuncID    VARCHAR(20)  - 上層功能代碼
	@Level           TINYINT      - 階層
	@SystemType      VARCHAR(20)  - 系統類別
	@RouteName       VARCHAR(50)  - 路由名稱
	@DisplayOrder    INT          - 顯示順序
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@ParentFuncID VARCHAR(20)
	,@Level TINYINT
	,@SystemType VARCHAR(20)
	,@RouteName VARCHAR(50)
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @FuncID = ''
	SET @FuncName = ''
	SET @ParentFuncID = ''
	SET @Level = ''
	SET @SystemType = ''
	SET @RouteName = ''
	SET @DisplayOrder = ''
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspFuncInsert] 
		@FuncID = @FuncID
		,@FuncName = @FuncName
		,@ParentFuncID = @ParentFuncID
		,@Level = @Level
		,@SystemType = @SystemType
		,@RouteName = @RouteName
		,@DisplayOrder = @DisplayOrder
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
** 2022/04/11 14:11:34    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncInsert] (
	@FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@ParentFuncID VARCHAR(20)
	,@Level TINYINT
	,@SystemType VARCHAR(20)
	,@RouteName VARCHAR(50)
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbFunc] (
			FuncID
			,FuncName
			,ParentFuncID
			,Level
			,SystemType
			,RouteName
			,DisplayOrder
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@FuncID
			,@FuncName
			,@ParentFuncID
			,@Level
			,@SystemType
			,@RouteName
			,@DisplayOrder
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
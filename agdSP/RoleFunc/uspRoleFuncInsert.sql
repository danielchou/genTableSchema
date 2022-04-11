/****************************************************************
** Name: [agdSp].[uspRoleFuncInsert]
** Desc: 角色功能配對新增
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
	@RoleID          VARCHAR(20)  - 角色代碼
	@FuncID          VARCHAR(20)  - 功能代碼
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @RoleID = ''
	SET @FuncID = ''
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspRoleFuncInsert] 
		@RoleID = @RoleID
		,@FuncID = @FuncID
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
CREATE PROCEDURE [agdSp].[uspRoleFuncInsert] (
	@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbRoleFunc] (
			RoleID
			,FuncID
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@RoleID
			,@FuncID
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
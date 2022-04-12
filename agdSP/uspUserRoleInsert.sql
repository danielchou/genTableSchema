/****************************************************************
** Name: [agdSp].[uspUserRoleInsert]
** Desc: 使用者角色配對新增
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
	@UserID          VARCHAR(20)  - 使用者帳號
	@RoleID          VARCHAR(20)  - 角色代碼
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @UserID = 'admin'
	SET @RoleID = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspUserRoleInsert] 
		@UserID = @UserID
		,@RoleID = @RoleID
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
** 2022/04/12 16:52:47    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserRoleInsert] (
	@UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbUserRole] (
			UserID
			,RoleID
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@UserID
			,@RoleID
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
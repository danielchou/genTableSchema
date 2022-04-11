/****************************************************************
** Name: [agdSp].[uspRoleInsert]
** Desc: 角色新增
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
	@RoleName        NVARCHAR(50) - 角色名稱
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
	,@RoleName NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @RoleID = ''
	SET @RoleName = ''
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspRoleInsert] 
		@RoleID = @RoleID
		,@RoleName = @RoleName
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
CREATE PROCEDURE [agdSp].[uspRoleInsert] (
	@RoleID VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbRole] (
			RoleID
			,RoleName
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@RoleID
			,@RoleName
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
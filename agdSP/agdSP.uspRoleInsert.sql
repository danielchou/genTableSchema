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
	@RoleId VARCHAR(20)	-	角色代碼
	@RoleName NVARCHAR(50)	-	角色名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @RoleId = '1'
	SET @RoleName = 'admin'
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspRoleInsert] 
	 @RoleId = @RoleId
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
** 2022-03-22 23:44:29    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspRoleInsert] (
	@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)    
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
			[RoleId]
			,[RoleName]
			,[CreateDT]
			,[Creator]
			,[UpdateDT]
			,[Updator]
        )
		VALUES (
			@RoleId
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

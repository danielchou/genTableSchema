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
	@RoleId	VARCHAR(20) - 角色代碼
	@RoleName	NVARCHAR(50) - 角色名稱
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
	,@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @RoleId = 'R01'
	SET @RoleName = 'admin'
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspRoleInsert] 
	 @RoleId = @RoleId
	,@RoleName = @RoleName
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
** 2022-04-01 13:51:29    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspRoleInsert] (
	@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@IsEnable BIT    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbRole] (
			RoleId
			,RoleName
			,IsEnable
			,CreateDt
			,UpdateDt
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@RoleId
			,@RoleName
			,@IsEnable
			,@CreateDt
			,@UpdateDt
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
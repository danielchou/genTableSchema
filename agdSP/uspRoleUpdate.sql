/****************************************************************
** Name: [agdSp].[uspRoleUpdate]
** Desc: 角色更新
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
    @SeqNo	INT - 流水號
	@RoleId	VARCHAR(20) - 角色代碼
	@RoleName	NVARCHAR(50) - 角色名稱
	@IsEnable	BIT - 是否啟用?
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@IsEnable BIT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @RoleId = 'R01'
	SET @RoleName = 'admin'
	SET @IsEnable = 1

EXEC @return_value = [agdSp].[uspRoleUpdate]
    @SeqNo = @SeqNo
	,@RoleId = @RoleId
	,@RoleName = @RoleName
	,@IsEnable = @IsEnable
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-28 12:29:50    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspRoleUpdate] (
	@SeqNo INT
	,@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@IsEnable BIT
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbRole
		SET RoleId = @RoleId
			,RoleName = @RoleName
			,IsEnable = @IsEnable
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
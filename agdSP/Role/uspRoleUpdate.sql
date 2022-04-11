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
    @SeqNo           INT          - 流水號
	@RoleID          VARCHAR(20)  - 角色代碼
	@RoleName        NVARCHAR(50) - 角色名稱
	@Updator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@RoleID VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @RoleID = ''
	SET @RoleName = ''
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspRoleUpdate]
    @SeqNo = @SeqNo
		,@RoleID = @RoleID
		,@RoleName = @RoleName
	,@Updator = @Updator
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
CREATE PROCEDURE [agdSp].[uspRoleUpdate] (
	@SeqNo INT
	,@RoleID VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbRole
		SET RoleID = @RoleID
			,RoleName = @RoleName
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
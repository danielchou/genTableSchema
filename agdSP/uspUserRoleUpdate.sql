/****************************************************************
** Name: [agdSp].[uspUserRoleUpdate]
** Desc: 使用者角色配對更新
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
	@UserID          VARCHAR(20)  - 使用者帳號
	@RoleID          VARCHAR(20)  - 角色代碼
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
	,@UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @UserID = 'admin'
	SET @RoleID = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspUserRoleUpdate]
    @SeqNo = @SeqNo
		,@UserID = @UserID
		,@RoleID = @RoleID
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
** 2022/04/12 16:52:47    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserRoleUpdate] (
	@SeqNo INT
	,@UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbUserRole
		SET UserID = @UserID
			,RoleID = @RoleID
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
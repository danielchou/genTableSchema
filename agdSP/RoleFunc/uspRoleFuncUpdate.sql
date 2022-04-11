/****************************************************************
** Name: [agdSp].[uspRoleFuncUpdate]
** Desc: 角色功能配對更新
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
	@FuncID          VARCHAR(20)  - 功能代碼
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
	,@FuncID VARCHAR(20)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @RoleID = ''
	SET @FuncID = ''
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspRoleFuncUpdate]
    @SeqNo = @SeqNo
		,@RoleID = @RoleID
		,@FuncID = @FuncID
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
CREATE PROCEDURE [agdSp].[uspRoleFuncUpdate] (
	@SeqNo INT
	,@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbRoleFunc
		SET RoleID = @RoleID
			,FuncID = @FuncID
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
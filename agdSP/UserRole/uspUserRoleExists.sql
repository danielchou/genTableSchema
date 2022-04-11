/****************************************************************
** Name: [agdSp].[uspUserRoleExists]
** Desc: 使用者角色配對查詢是否重複
**
** Return values: 0 成功
** Return Recordset: 
**	Total		:資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@UserID          VARCHAR(20)  - 使用者帳號
	@RoleID          VARCHAR(20)  - 角色代碼
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
    ,@ErrorMsg NVARCHAR(100)

    SET @UserID = ''
	SET @RoleID = ''

	EXEC @return_value = [agdSp].[uspUserRoleExists] 
    	@UserID = @UserID
		,@RoleID = @RoleID
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/11 14:11:34    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserRoleExists]
    @UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbUserRole
		WHERE SeqNo != @SeqNo
			AND ( 
                UserID = @UserID OR
				RoleID = @RoleID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
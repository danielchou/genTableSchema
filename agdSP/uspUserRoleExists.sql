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
	@SeqNo           INT          - 流水號
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
    ,@SeqNo INT
	,@UserID VARCHAR(20)
	,@RoleID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @UserID = 'admin'
	SET @RoleID = '1'

	EXEC @return_value = [agdSp].[uspUserRoleExists] 
    	@SeqNo = @SeqNo
		,@UserID = @UserID
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
** 2022/04/12 16:52:47    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserRoleExists]
    @SeqNo INT
	,@UserID VARCHAR(20)
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
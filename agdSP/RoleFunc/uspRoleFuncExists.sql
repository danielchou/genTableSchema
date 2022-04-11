/****************************************************************
** Name: [agdSp].[uspRoleFuncExists]
** Desc: 角色功能配對查詢是否重複
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
	@RoleID          VARCHAR(20)  - 角色代碼
	@FuncID          VARCHAR(20)  - 功能代碼
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @RoleID = ''
	SET @FuncID = ''

	EXEC @return_value = [agdSp].[uspRoleFuncExists] 
    	@RoleID = @RoleID
		,@FuncID = @FuncID
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
CREATE PROCEDURE [agdSp].[uspRoleFuncExists]
    @RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbRoleFunc
		WHERE SeqNo != @SeqNo
			AND ( 
                RoleID = @RoleID OR
				FuncID = @FuncID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
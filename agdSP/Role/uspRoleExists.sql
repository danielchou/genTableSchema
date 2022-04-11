/****************************************************************
** Name: [agdSp].[uspRoleExists]
** Desc: 角色查詢是否重複
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
	@RoleName        NVARCHAR(50) - 角色名稱
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
    ,@ErrorMsg NVARCHAR(100)

    SET @RoleID = ''
	SET @RoleName = ''

	EXEC @return_value = [agdSp].[uspRoleExists] 
    	@RoleID = @RoleID
		,@RoleName = @RoleName
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
CREATE PROCEDURE [agdSp].[uspRoleExists]
    @RoleID VARCHAR(20)
	,@RoleName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbRole
		WHERE SeqNo != @SeqNo
			AND ( 
                RoleID = @RoleID OR
				RoleName = @RoleName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
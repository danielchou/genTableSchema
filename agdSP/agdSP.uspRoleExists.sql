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
    ,@ErrorMsg NVARCHAR(100)

    SET @RoleId = 1
	SET @RoleName = 'admin'

EXEC @return_value = [agdSp].[uspRoleExists] 
    @RoleId = @RoleId
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
** 2022-03-22 00:40:39    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspRoleExists]
    @RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbRole
		WHERE SeqNo != @SeqNo
			AND ( 
                RoleId = @RoleId OR
				RoleName = @RoleName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

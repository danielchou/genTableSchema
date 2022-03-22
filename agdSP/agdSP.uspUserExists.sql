/****************************************************************
** Name: [agdSp].[uspUserExists]
** Desc: 使用者查詢是否重複
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
	@SeqNo INT	-	使用者序號
	@UserId NVARCHAR(20)	-	使用者帳號
	@UserName NVARCHAR(50)	-	使用者名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@UserId NVARCHAR(20)
	,@UserName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 2
	SET @UserId = 'agent'
	SET @UserName = 'BBB'

EXEC @return_value = [agdSp].[uspUserExists] 
    @SeqNo = @SeqNo
	,@UserId = @UserId
	,@UserName = @UserName
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 23:44:27    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspUserExists]
    @SeqNo INT
	,@UserId NVARCHAR(20)
	,@UserName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbUser
		WHERE SeqNo != @SeqNo
			AND ( 
                UserId = @UserId OR
				UserName = @UserName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

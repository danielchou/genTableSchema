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
	@SeqNo           INT          - 流水號
	@UserID          VARCHAR(20)  - 使用者帳號
	@AgentLoginID    VARCHAR(10)  - CTI登入帳號
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
	,@AgentLoginID VARCHAR(10)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @UserID = 'admin'
	SET @AgentLoginID = '1234'

	EXEC @return_value = [agdSp].[uspUserExists] 
    	@SeqNo = @SeqNo
		,@UserID = @UserID
		,@AgentLoginID = @AgentLoginID
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
CREATE PROCEDURE [agdSp].[uspUserExists]
    @SeqNo INT
	,@UserID VARCHAR(20)
	,@AgentLoginID VARCHAR(10)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbUser
		WHERE SeqNo != @SeqNo
			AND ( 
                UserID = @UserID OR
				AgentLoginID = @AgentLoginID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
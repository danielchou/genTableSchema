/****************************************************************
** Name: [agdSp].[uspFuncExists]
** Desc: 功能查詢是否重複
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
	@FuncID          VARCHAR(20)  - 功能代碼
	@FuncName        NVARCHAR(50) - 功能名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @FuncID = ''
	SET @FuncName = ''

	EXEC @return_value = [agdSp].[uspFuncExists] 
    	@FuncID = @FuncID
		,@FuncName = @FuncName
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
CREATE PROCEDURE [agdSp].[uspFuncExists]
    @FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbFunc
		WHERE SeqNo != @SeqNo
			AND ( 
                FuncID = @FuncID OR
				FuncName = @FuncName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
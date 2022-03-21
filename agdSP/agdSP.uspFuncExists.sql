/****************************************************************
** Name: [agdSp].[uspFuncExists]
** Desc: 系統功能查詢是否重複
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
	@FuncId VARCHAR(20)	-	功能代碼
	@FuncName NVARCHAR(50)	-	功能名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @FuncId = 1
	SET @FuncName = '代碼設定'

EXEC @return_value = [agdSp].[uspFuncExists] 
    @FuncId = @FuncId
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
** 2022-03-22 00:40:39    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspFuncExists]
    @FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbFunc
		WHERE SeqNo != @SeqNo
			AND ( 
                FuncId = @FuncId OR
				FuncName = @FuncName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

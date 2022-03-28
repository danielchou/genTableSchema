/****************************************************************
** Name: [agdSp].[uspCodeExists]
** Desc: 系統代碼查詢是否重複
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
	@SeqNo	INT - 流水號
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1

EXEC @return_value = [agdSp].[uspCodeExists] 
    @SeqNo = @SeqNo
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-28 14:45:45    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeExists]
    @SeqNo INT
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbCode
		WHERE SeqNo != @SeqNo
			AND ( 
                
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
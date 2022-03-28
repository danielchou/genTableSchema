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
	@CodeType	NVARCHAR(20) - 代碼分類
	@CodeId	VARCHAR(20) - 系統代碼檔代碼
	@CodeName	NVARCHAR(50) - 系統代碼檔名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'

EXEC @return_value = [agdSp].[uspCodeExists] 
    @SeqNo = @SeqNo
	,@CodeType = @CodeType
	,@CodeId = @CodeId
	,@CodeName = @CodeName
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-28 12:29:50    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeExists]
    @SeqNo INT
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
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
                CodeType = @CodeType OR
				CodeId = @CodeId OR
				CodeName = @CodeName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
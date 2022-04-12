/****************************************************************
** Name: [agdSp].[uspCodeExists]
** Desc: 共用碼查詢是否重複
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
	@CodeType        VARCHAR(20)  - 共用碼類別
	@CodeID          VARCHAR(20)  - 共用碼代碼
	@CodeName        NVARCHAR(50) - 共用碼名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @CodeType = '1234'
	SET @CodeID = '1234'
	SET @CodeName = '1234'

	EXEC @return_value = [agdSp].[uspCodeExists] 
    	@SeqNo = @SeqNo
		,@CodeType = @CodeType
		,@CodeID = @CodeID
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
** 2022/04/12 16:52:48    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeExists]
    @SeqNo INT
	,@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
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
				CodeID = @CodeID OR
				CodeName = @CodeName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
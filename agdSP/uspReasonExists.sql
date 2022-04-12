/****************************************************************
** Name: [agdSp].[uspReasonExists]
** Desc: 聯繫原因碼查詢是否重複
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
	@ReasonID        VARCHAR(20)  - 聯繫原因碼代碼
	@ReasonName      NVARCHAR(50) - 聯繫原因碼名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ReasonID = '1234'
	SET @ReasonName = '1234'

	EXEC @return_value = [agdSp].[uspReasonExists] 
    	@SeqNo = @SeqNo
		,@ReasonID = @ReasonID
		,@ReasonName = @ReasonName
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
CREATE PROCEDURE [agdSp].[uspReasonExists]
    @SeqNo INT
	,@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbReason
		WHERE SeqNo != @SeqNo
			AND ( 
                ReasonID = @ReasonID OR
				ReasonName = @ReasonName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
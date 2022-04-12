/****************************************************************
** Name: [agdSp].[uspReasonIvrExists]
** Desc: 聯繫原因Ivr配對查詢是否重複
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
	@IvrID           VARCHAR(20)  - Ivr代碼
	@ReasonID        VARCHAR(20)  - 原因代碼
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @IvrID = '1234'
	SET @ReasonID = '1234'

	EXEC @return_value = [agdSp].[uspReasonIvrExists] 
    	@SeqNo = @SeqNo
		,@IvrID = @IvrID
		,@ReasonID = @ReasonID
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
CREATE PROCEDURE [agdSp].[uspReasonIvrExists]
    @SeqNo INT
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbReasonIvr
		WHERE SeqNo != @SeqNo
			AND ( 
                IvrID = @IvrID OR
				ReasonID = @ReasonID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
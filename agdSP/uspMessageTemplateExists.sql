/****************************************************************
** Name: [agdSp].[uspMessageTemplateExists]
** Desc: 訊息傳送範本查詢是否重複
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
	@MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@MessageTemplateID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @MessageTemplateID = '1234'

	EXEC @return_value = [agdSp].[uspMessageTemplateExists] 
    	@SeqNo = @SeqNo
		,@MessageTemplateID = @MessageTemplateID
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:50    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMessageTemplateExists]
    @SeqNo INT
	,@MessageTemplateID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbMessageTemplate
		WHERE SeqNo != @SeqNo
			AND ( 
                MessageTemplateID = @MessageTemplateID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
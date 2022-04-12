/****************************************************************
** Name: [agdSp].[uspMessageTemplateDelete]
** Desc: 訊息傳送範本刪除
**
** Return values: 0 成功
** Return Recordset: 
**	NA
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - 流水號
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

	EXEC @return_value = [agdSp].[uspMessageTemplateDelete] 
		@SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:		 	Author:				Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:50 	Jerry Yang			first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMessageTemplateDelete] (
	@SeqNo INT
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		DELETE agdSet.tbMessageTemplate
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
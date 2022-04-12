/****************************************************************
** Name: [agdSp].[uspNotificationTemplateExists]
** Desc: 通知公告範本查詢是否重複
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
	@NotificationID  VARCHAR(50)  - 通知公告代碼
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@NotificationID VARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @NotificationID = '1234'

	EXEC @return_value = [agdSp].[uspNotificationTemplateExists] 
    	@SeqNo = @SeqNo
		,@NotificationID = @NotificationID
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:49    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspNotificationTemplateExists]
    @SeqNo INT
	,@NotificationID VARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbNotificationTemplate
		WHERE SeqNo != @SeqNo
			AND ( 
                NotificationID = @NotificationID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
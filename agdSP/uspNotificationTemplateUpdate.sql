/****************************************************************
** Name: [agdSp].[uspNotificationTemplateUpdate]
** Desc: 通知公告範本更新
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
	@NotificationType VARCHAR(3)   - 通知公告類別
	@NotificationID  VARCHAR(50)  - 通知公告代碼
	@NotificationName NVARCHAR(50) - 通知公告名稱
	@Content         NVARCHAR(2000) - 通知公告範本
	@DisplayOrder    INT          - 顯示順序
	@IsEnable        BIT          - 是否啟用?
	@Updator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
	,@Content NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @NotificationType = '1'
	SET @NotificationID = '1234'
	SET @NotificationName = '1234'
	SET @Content = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspNotificationTemplateUpdate]
    @SeqNo = @SeqNo
		,@NotificationType = @NotificationType
		,@NotificationID = @NotificationID
		,@NotificationName = @NotificationName
		,@Content = @Content
		,@DisplayOrder = @DisplayOrder
		,@IsEnable = @IsEnable
	,@Updator = @Updator
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:50    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspNotificationTemplateUpdate] (
	@SeqNo INT
	,@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
	,@Content NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbNotificationTemplate
		SET NotificationType = @NotificationType
			,NotificationID = @NotificationID
			,NotificationName = @NotificationName
			,Content = @Content
			,DisplayOrder = @DisplayOrder
			,IsEnable = @IsEnable
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
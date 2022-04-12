/****************************************************************
** Name: [agdSp].[uspNotificationTemplateInsert]
** Desc: 通知公告範本新增
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
	@NotificationType VARCHAR(3)   - 通知公告類別
	@NotificationID  VARCHAR(50)  - 通知公告代碼
	@NotificationName NVARCHAR(50) - 通知公告名稱
	@Content         NVARCHAR(2000) - 通知公告範本
	@DisplayOrder    INT          - 顯示順序
	@IsEnable        BIT          - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
	,@Content NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @NotificationType = '1'
	SET @NotificationID = '1234'
	SET @NotificationName = '1234'
	SET @Content = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspNotificationTemplateInsert] 
		@NotificationType = @NotificationType
		,@NotificationID = @NotificationID
		,@NotificationName = @NotificationName
		,@Content = @Content
		,@DisplayOrder = @DisplayOrder
		,@IsEnable = @IsEnable
		,@Creator = @Creator
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
CREATE PROCEDURE [agdSp].[uspNotificationTemplateInsert] (
	@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
	,@Content NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbNotificationTemplate] (
			NotificationType
			,NotificationID
			,NotificationName
			,Content
			,DisplayOrder
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@NotificationType
			,@NotificationID
			,@NotificationName
			,@Content
			,@DisplayOrder
			,@IsEnable
			,GETDATE()
			,@Creator
			,GETDATE()
			,@Creator
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
/****************************************************************
** Name: [agdSp].[uspMessageTemplateInsert]
** Desc: 訊息傳送範本新增
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
	@MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
	@MessageTemplateName NVARCHAR(50) - 訊息傳送範本名稱
	@MessageB08Code  VARCHAR(20)  - 訊息傳送B08代碼
	@Content         NVARCHAR(2000) - 訊息傳送範本
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
	,@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
	,@Content NVARCHAR(2000)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @MessageTemplateID = '1234'
	SET @MessageTemplateName = '1234'
	SET @MessageB08Code = '1234'
	SET @Content = '1234'
	SET @IsEnable = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspMessageTemplateInsert] 
		@MessageTemplateID = @MessageTemplateID
		,@MessageTemplateName = @MessageTemplateName
		,@MessageB08Code = @MessageB08Code
		,@Content = @Content
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
CREATE PROCEDURE [agdSp].[uspMessageTemplateInsert] (
	@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
	,@Content NVARCHAR(2000)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbMessageTemplate] (
			MessageTemplateID
			,MessageTemplateName
			,MessageB08Code
			,Content
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@MessageTemplateID
			,@MessageTemplateName
			,@MessageB08Code
			,@Content
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
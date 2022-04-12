/****************************************************************
** Name: [agdSp].[uspMessageTemplateUpdate]
** Desc: 訊息傳送範本更新
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
	@MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
	@MessageTemplateName NVARCHAR(50) - 訊息傳送範本名稱
	@MessageB08Code  VARCHAR(20)  - 訊息傳送B08代碼
	@Content         NVARCHAR(2000) - 訊息傳送範本
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
	,@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
	,@Content NVARCHAR(2000)
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @MessageTemplateID = '1234'
	SET @MessageTemplateName = '1234'
	SET @MessageB08Code = '1234'
	SET @Content = '1234'
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspMessageTemplateUpdate]
    @SeqNo = @SeqNo
		,@MessageTemplateID = @MessageTemplateID
		,@MessageTemplateName = @MessageTemplateName
		,@MessageB08Code = @MessageB08Code
		,@Content = @Content
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
CREATE PROCEDURE [agdSp].[uspMessageTemplateUpdate] (
	@SeqNo INT
	,@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
	,@Content NVARCHAR(2000)
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbMessageTemplate
		SET MessageTemplateID = @MessageTemplateID
			,MessageTemplateName = @MessageTemplateName
			,MessageB08Code = @MessageB08Code
			,Content = @Content
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
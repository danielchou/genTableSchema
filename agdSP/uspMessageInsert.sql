/****************************************************************
** Name: [agdSp].[uspMessageInsert]
** Desc: 訊息傳送新增
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
	@MessageSheetID  VARCHAR(20)  - 訊息傳送頁籤代碼
	@MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
	@MessageName     NVARCHAR(50) - 訊息傳送名稱
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
	,@MessageSheetID VARCHAR(20)
	,@MessageTemplateID VARCHAR(20)
	,@MessageName NVARCHAR(50)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @MessageSheetID = '1234'
	SET @MessageTemplateID = '1234'
	SET @MessageName = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspMessageInsert] 
		@MessageSheetID = @MessageSheetID
		,@MessageTemplateID = @MessageTemplateID
		,@MessageName = @MessageName
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
CREATE PROCEDURE [agdSp].[uspMessageInsert] (
	@MessageSheetID VARCHAR(20)
	,@MessageTemplateID VARCHAR(20)
	,@MessageName NVARCHAR(50)
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
	INSERT INTO [agdSet].[tbMessage] (
			MessageSheetID
			,MessageTemplateID
			,MessageName
			,DisplayOrder
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@MessageSheetID
			,@MessageTemplateID
			,@MessageName
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
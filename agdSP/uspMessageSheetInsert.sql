/****************************************************************
** Name: [agdSp].[uspMessageSheetInsert]
** Desc: 訊息傳送頁籤新增
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
	@MessageSheetType VARCHAR(1)   - 訊息傳送頁籤類別
	@MessageSheetID  VARCHAR(20)  - 訊息傳送頁籤代碼
	@MessageSheetName NVARCHAR(50) - 訊息傳送頁籤名稱
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
	,@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @MessageSheetType = 'S'
	SET @MessageSheetID = '1234'
	SET @MessageSheetName = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspMessageSheetInsert] 
		@MessageSheetType = @MessageSheetType
		,@MessageSheetID = @MessageSheetID
		,@MessageSheetName = @MessageSheetName
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
CREATE PROCEDURE [agdSp].[uspMessageSheetInsert] (
	@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
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
	INSERT INTO [agdSet].[tbMessageSheet] (
			MessageSheetType
			,MessageSheetID
			,MessageSheetName
			,DisplayOrder
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@MessageSheetType
			,@MessageSheetID
			,@MessageSheetName
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
/****************************************************************
** Name: [agdSp].[uspMessageSheetUpdate]
** Desc: 訊息傳送頁籤更新
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
	@MessageSheetType VARCHAR(1)   - 訊息傳送頁籤類別
	@MessageSheetID  VARCHAR(20)  - 訊息傳送頁籤代碼
	@MessageSheetName NVARCHAR(50) - 訊息傳送頁籤名稱
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
	,@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @MessageSheetType = 'S'
	SET @MessageSheetID = '1234'
	SET @MessageSheetName = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspMessageSheetUpdate]
    @SeqNo = @SeqNo
		,@MessageSheetType = @MessageSheetType
		,@MessageSheetID = @MessageSheetID
		,@MessageSheetName = @MessageSheetName
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
CREATE PROCEDURE [agdSp].[uspMessageSheetUpdate] (
	@SeqNo INT
	,@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
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
		UPDATE agdSet.tbMessageSheet
		SET MessageSheetType = @MessageSheetType
			,MessageSheetID = @MessageSheetID
			,MessageSheetName = @MessageSheetName
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
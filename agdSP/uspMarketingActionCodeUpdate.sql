/****************************************************************
** Name: [agdSp].[uspMarketingActionCodeUpdate]
** Desc: 行銷方案結果更新
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
	@ActionCodeType  VARCHAR(20)  - 客群方案類別
	@MarketingID     VARCHAR(20)  - 行銷方案代碼
	@ActionCode      VARCHAR(20)  - 行銷結果代碼
	@Content         NVARCHAR(200) - 行銷結果說明
	@IsAccept        BIT          - 是否接受?
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
	,@ActionCodeType VARCHAR(20)
	,@MarketingID VARCHAR(20)
	,@ActionCode VARCHAR(20)
	,@Content NVARCHAR(200)
	,@IsAccept BIT
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ActionCodeType = '1234'
	SET @MarketingID = '1234'
	SET @ActionCode = '1234'
	SET @Content = '1234'
	SET @IsAccept = '1'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspMarketingActionCodeUpdate]
    @SeqNo = @SeqNo
		,@ActionCodeType = @ActionCodeType
		,@MarketingID = @MarketingID
		,@ActionCode = @ActionCode
		,@Content = @Content
		,@IsAccept = @IsAccept
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
** 2022/04/12 16:52:51    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMarketingActionCodeUpdate] (
	@SeqNo INT
	,@ActionCodeType VARCHAR(20)
	,@MarketingID VARCHAR(20)
	,@ActionCode VARCHAR(20)
	,@Content NVARCHAR(200)
	,@IsAccept BIT
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
		UPDATE agdSet.tbMarketingActionCode
		SET ActionCodeType = @ActionCodeType
			,MarketingID = @MarketingID
			,ActionCode = @ActionCode
			,Content = @Content
			,IsAccept = @IsAccept
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
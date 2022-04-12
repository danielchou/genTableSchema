/****************************************************************
** Name: [agdSp].[uspMarketingInsert]
** Desc: 行銷方案新增
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
	@MarketingID     VARCHAR(20)  - 行銷方案代碼
	@MarketingType   VARCHAR(1)   - 行銷方案類別
	@MarketingName   NVARCHAR(50) - 行銷方案名稱
	@Content         NVARCHAR(100) - 行銷方案內容
	@MarketingScript NVARCHAR(2000) - 行銷方案話術
	@MarketingBegintDT DATETIME2(7) - 開始日期
	@MarketingEndDT  DATETIME2(7) - 結束日期
	@OfferCode       VARCHAR(20)  - 專案識別碼
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
	,@MarketingID VARCHAR(20)
	,@MarketingType VARCHAR(1)
	,@MarketingName NVARCHAR(50)
	,@Content NVARCHAR(100)
	,@MarketingScript NVARCHAR(2000)
	,@MarketingBegintDT DATETIME2(7)
	,@MarketingEndDT DATETIME2(7)
	,@OfferCode VARCHAR(20)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @MarketingID = '1234'
	SET @MarketingType = '1'
	SET @MarketingName = '1234'
	SET @Content = '1234'
	SET @MarketingScript = '1234'
	SET @MarketingBegintDT = '2022-02-02 12:00:00'
	SET @MarketingEndDT = '2022-02-02 12:00:00'
	SET @OfferCode = '1'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspMarketingInsert] 
		@MarketingID = @MarketingID
		,@MarketingType = @MarketingType
		,@MarketingName = @MarketingName
		,@Content = @Content
		,@MarketingScript = @MarketingScript
		,@MarketingBegintDT = @MarketingBegintDT
		,@MarketingEndDT = @MarketingEndDT
		,@OfferCode = @OfferCode
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
CREATE PROCEDURE [agdSp].[uspMarketingInsert] (
	@MarketingID VARCHAR(20)
	,@MarketingType VARCHAR(1)
	,@MarketingName NVARCHAR(50)
	,@Content NVARCHAR(100)
	,@MarketingScript NVARCHAR(2000)
	,@MarketingBegintDT DATETIME2(7)
	,@MarketingEndDT DATETIME2(7)
	,@OfferCode VARCHAR(20)
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
	INSERT INTO [agdSet].[tbMarketing] (
			MarketingID
			,MarketingType
			,MarketingName
			,Content
			,MarketingScript
			,MarketingBegintDT
			,MarketingEndDT
			,OfferCode
			,DisplayOrder
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@MarketingID
			,@MarketingType
			,@MarketingName
			,@Content
			,@MarketingScript
			,@MarketingBegintDT
			,@MarketingEndDT
			,@OfferCode
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
/****************************************************************
** Name: [agdSp].[uspMarketingGet]
** Desc: 行銷方案查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** MarketingID      VARCHAR(20)  - 行銷方案代碼
** MarketingType    VARCHAR(1)   - 行銷方案類別
** MarketingName    NVARCHAR(50) - 行銷方案名稱
** Content          NVARCHAR(100) - 行銷方案內容
** MarketingScript  NVARCHAR(2000) - 行銷方案話術
** MarketingBegintDT DATETIME2(7) - 開始日期
** MarketingEndDT   DATETIME2(7) - 結束日期
** OfferCode        VARCHAR(20)  - 專案識別碼
** DisplayOrder     INT          - 顯示順序
** IsEnable         BIT          - 是否啟用?
** CreateDT         DATETIME2(7) - 建立時間
** Creator          VARCHAR(20)  - 建立者
** UpdateDT         DATETIME2(7) - 更新時間
** Updator          VARCHAR(20)  - 更新者
**	UpdatorName       NVARCHAR(20)   - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - 流水號
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
	DECLARE @return_value INT
		,@SeqNo INT
		,@ErrorMsg NVARCHAR(100)

	SET @SeqNo = 1

	EXEC @return_value = [agdSp].[uspMarketingGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:50    	Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMarketingGet] (
	@SeqNo INT
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
			f.SeqNo
			,f.MarketingID
			,f.MarketingType
			,f.MarketingName
			,f.Content
			,f.MarketingScript
			,f.MarketingBegintDT
			,f.MarketingEndDT
			,f.OfferCode
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbMarketing AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
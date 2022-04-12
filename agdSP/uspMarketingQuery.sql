/****************************************************************
** Name: agdSp.uspMarketingQuery
** Desc: 行銷方案進階查詢
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
** UpdatorName      NVARCHAR(20) - 更新者名稱
** Total            INT          - 資料總筆數
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
	@Page 			  INT 			- 頁數
	@RowsPerPage 	  INT 			- 每頁筆數
	@SortColumn 	  NVARCHAR(30) 	- 排序欄位
	@SortOrder 		  VARCHAR(10) 	- 排序順序
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
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @MarketingID = '1234'
	SET @MarketingType = '1'
	SET @MarketingName = '1234'

EXEC @return_value = agdSp.uspMarketingQuery
	@MarketingID = @MarketingID
		,@MarketingType = @MarketingType
		,@MarketingName = @MarketingName
	,@Page = @Page
	,@RowsPerPage = @RowsPerPage
	,@SortColumn = @SortColumn
	,@SortOrder = @SortOrder
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:50    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMarketingQuery] (
	@MarketingID VARCHAR(20)
	,@MarketingType VARCHAR(1)
	,@MarketingName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
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
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbMarketing AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.MarketingID LIKE CASE WHEN @MarketingID = '' THEN f.MarketingID ELSE '%' + @MarketingID + '%' END
			AND f.MarketingType LIKE CASE WHEN @MarketingType = '' THEN f.MarketingType ELSE '%' + @MarketingType + '%' END
			AND f.MarketingName LIKE CASE WHEN @MarketingName = '' THEN f.MarketingName ELSE '%' + @MarketingName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'MarketingID' AND @SortOrder = 'ASC' THEN f.MarketingID END ASC,
			CASE WHEN @SortColumn = 'MarketingID' AND @SortOrder = 'DESC' THEN f.MarketingID END DESC,
			CASE WHEN @SortColumn = 'MarketingType' AND @SortOrder = 'ASC' THEN f.MarketingType END ASC,
			CASE WHEN @SortColumn = 'MarketingType' AND @SortOrder = 'DESC' THEN f.MarketingType END DESC,
			CASE WHEN @SortColumn = 'MarketingName' AND @SortOrder = 'ASC' THEN f.MarketingName END ASC,
			CASE WHEN @SortColumn = 'MarketingName' AND @SortOrder = 'DESC' THEN f.MarketingName END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
			CASE WHEN @SortColumn = 'MarketingScript' AND @SortOrder = 'ASC' THEN f.MarketingScript END ASC,
			CASE WHEN @SortColumn = 'MarketingScript' AND @SortOrder = 'DESC' THEN f.MarketingScript END DESC,
			CASE WHEN @SortColumn = 'MarketingBegintDT' AND @SortOrder = 'ASC' THEN f.MarketingBegintDT END ASC,
			CASE WHEN @SortColumn = 'MarketingBegintDT' AND @SortOrder = 'DESC' THEN f.MarketingBegintDT END DESC,
			CASE WHEN @SortColumn = 'MarketingEndDT' AND @SortOrder = 'ASC' THEN f.MarketingEndDT END ASC,
			CASE WHEN @SortColumn = 'MarketingEndDT' AND @SortOrder = 'DESC' THEN f.MarketingEndDT END DESC,
			CASE WHEN @SortColumn = 'OfferCode' AND @SortOrder = 'ASC' THEN f.OfferCode END ASC,
			CASE WHEN @SortColumn = 'OfferCode' AND @SortOrder = 'DESC' THEN f.OfferCode END DESC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'ASC' THEN f.DisplayOrder END ASC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'DESC' THEN f.DisplayOrder END DESC,
			CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'ASC' THEN f.IsEnable END ASC,
			CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'DESC' THEN f.IsEnable END DESC,
			CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'ASC' THEN f.UpdateDT END ASC,
			CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'DESC' THEN f.UpdateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
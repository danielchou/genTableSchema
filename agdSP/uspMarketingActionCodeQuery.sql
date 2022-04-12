/****************************************************************
** Name: agdSp.uspMarketingActionCodeQuery
** Desc: 行銷方案結果進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** ActionCodeType   VARCHAR(20)  - 客群方案類別
** MarketingID      VARCHAR(20)  - 行銷方案代碼
** ActionCode       VARCHAR(20)  - 行銷結果代碼
** Content          NVARCHAR(200) - 行銷結果說明
** IsAccept         BIT          - 是否接受?
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
    @ActionCodeType  VARCHAR(20)  - 客群方案類別
	@MarketingID     VARCHAR(20)  - 行銷方案代碼
	@ActionCode      VARCHAR(20)  - 行銷結果代碼
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
	,@ActionCodeType VARCHAR(20)
	,@MarketingID VARCHAR(20)
	,@ActionCode VARCHAR(20)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @ActionCodeType = '1234'
	SET @MarketingID = '1234'
	SET @ActionCode = '1234'

EXEC @return_value = agdSp.uspMarketingActionCodeQuery
	@ActionCodeType = @ActionCodeType
		,@MarketingID = @MarketingID
		,@ActionCode = @ActionCode
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
** 2022/04/12 16:52:51    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspMarketingActionCodeQuery] (
	@ActionCodeType VARCHAR(20)
	,@MarketingID VARCHAR(20)
	,@ActionCode VARCHAR(20)
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
			,f.ActionCodeType
			,f.MarketingID
			,f.ActionCode
			,f.Content
			,f.IsAccept
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbMarketingActionCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.ActionCodeType LIKE CASE WHEN @ActionCodeType = '' THEN f.ActionCodeType ELSE '%' + @ActionCodeType + '%' END
			AND f.MarketingID LIKE CASE WHEN @MarketingID = '' THEN f.MarketingID ELSE '%' + @MarketingID + '%' END
			AND f.ActionCode LIKE CASE WHEN @ActionCode = '' THEN f.ActionCode ELSE '%' + @ActionCode + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'ActionCodeType' AND @SortOrder = 'ASC' THEN f.ActionCodeType END ASC,
			CASE WHEN @SortColumn = 'ActionCodeType' AND @SortOrder = 'DESC' THEN f.ActionCodeType END DESC,
			CASE WHEN @SortColumn = 'MarketingID' AND @SortOrder = 'ASC' THEN f.MarketingID END ASC,
			CASE WHEN @SortColumn = 'MarketingID' AND @SortOrder = 'DESC' THEN f.MarketingID END DESC,
			CASE WHEN @SortColumn = 'ActionCode' AND @SortOrder = 'ASC' THEN f.ActionCode END ASC,
			CASE WHEN @SortColumn = 'ActionCode' AND @SortOrder = 'DESC' THEN f.ActionCode END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
			CASE WHEN @SortColumn = 'IsAccept' AND @SortOrder = 'ASC' THEN f.IsAccept END ASC,
			CASE WHEN @SortColumn = 'IsAccept' AND @SortOrder = 'DESC' THEN f.IsAccept END DESC,
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
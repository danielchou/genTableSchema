/****************************************************************
** Name: agdSp.uspCodeQuery
** Desc: 共用碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** CodeType         VARCHAR(20)  - 共用碼類別
** CodeID           VARCHAR(20)  - 共用碼代碼
** CodeName         NVARCHAR(50) - 共用碼名稱
** Content          NVARCHAR(500) - 共用碼內容
** Memo             NVARCHAR(200) - 備註
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
    @CodeType        VARCHAR(20)  - 共用碼類別
	@CodeID          VARCHAR(20)  - 共用碼代碼
	@CodeName        NVARCHAR(50) - 共用碼名稱
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
	,@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @CodeType = '1234'
	SET @CodeID = '1234'
	SET @CodeName = '1234'

EXEC @return_value = agdSp.uspCodeQuery
	@CodeType = @CodeType
		,@CodeID = @CodeID
		,@CodeName = @CodeName
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
** 2022/04/12 16:52:48    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeQuery] (
	@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
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
			,f.CodeType
			,f.CodeID
			,f.CodeName
			,f.Content
			,f.Memo
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.CodeType LIKE CASE WHEN @CodeType = '' THEN f.CodeType ELSE '%' + @CodeType + '%' END
			AND f.CodeID LIKE CASE WHEN @CodeID = '' THEN f.CodeID ELSE '%' + @CodeID + '%' END
			AND f.CodeName LIKE CASE WHEN @CodeName = '' THEN f.CodeName ELSE '%' + @CodeName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'CodeType' AND @SortOrder = 'ASC' THEN f.CodeType END ASC,
			CASE WHEN @SortColumn = 'CodeType' AND @SortOrder = 'DESC' THEN f.CodeType END DESC,
			CASE WHEN @SortColumn = 'CodeID' AND @SortOrder = 'ASC' THEN f.CodeID END ASC,
			CASE WHEN @SortColumn = 'CodeID' AND @SortOrder = 'DESC' THEN f.CodeID END DESC,
			CASE WHEN @SortColumn = 'CodeName' AND @SortOrder = 'ASC' THEN f.CodeName END ASC,
			CASE WHEN @SortColumn = 'CodeName' AND @SortOrder = 'DESC' THEN f.CodeName END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
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
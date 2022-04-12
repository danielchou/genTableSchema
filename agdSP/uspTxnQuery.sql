/****************************************************************
** Name: agdSp.uspTxnQuery
** Desc: 交易執行進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** TxnType          VARCHAR(20)  - 交易執行類別
** TxnID            VARCHAR(50)  - 交易執行代碼
** TxnName          NVARCHAR(50) - 交易執行名稱
** TxnScript        NVARCHAR(2000) - 交易執行話術
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
    @TxnType         VARCHAR(20)  - 交易執行類別
	@TxnID           VARCHAR(50)  - 交易執行代碼
	@TxnName         NVARCHAR(50) - 交易執行名稱
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
	,@TxnType VARCHAR(20)
	,@TxnID VARCHAR(50)
	,@TxnName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @TxnType = 'AGD'
	SET @TxnID = '1234'
	SET @TxnName = '1234'

EXEC @return_value = agdSp.uspTxnQuery
	@TxnType = @TxnType
		,@TxnID = @TxnID
		,@TxnName = @TxnName
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
CREATE PROCEDURE [agdSp].[uspTxnQuery] (
	@TxnType VARCHAR(20)
	,@TxnID VARCHAR(50)
	,@TxnName NVARCHAR(50)
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
			,f.TxnType
			,f.TxnID
			,f.TxnName
			,f.TxnScript
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbTxn AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.TxnType LIKE CASE WHEN @TxnType = '' THEN f.TxnType ELSE '%' + @TxnType + '%' END
			AND f.TxnID LIKE CASE WHEN @TxnID = '' THEN f.TxnID ELSE '%' + @TxnID + '%' END
			AND f.TxnName LIKE CASE WHEN @TxnName = '' THEN f.TxnName ELSE '%' + @TxnName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'TxnType' AND @SortOrder = 'ASC' THEN f.TxnType END ASC,
			CASE WHEN @SortColumn = 'TxnType' AND @SortOrder = 'DESC' THEN f.TxnType END DESC,
			CASE WHEN @SortColumn = 'TxnID' AND @SortOrder = 'ASC' THEN f.TxnID END ASC,
			CASE WHEN @SortColumn = 'TxnID' AND @SortOrder = 'DESC' THEN f.TxnID END DESC,
			CASE WHEN @SortColumn = 'TxnName' AND @SortOrder = 'ASC' THEN f.TxnName END ASC,
			CASE WHEN @SortColumn = 'TxnName' AND @SortOrder = 'DESC' THEN f.TxnName END DESC,
			CASE WHEN @SortColumn = 'TxnScript' AND @SortOrder = 'ASC' THEN f.TxnScript END ASC,
			CASE WHEN @SortColumn = 'TxnScript' AND @SortOrder = 'DESC' THEN f.TxnScript END DESC,
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
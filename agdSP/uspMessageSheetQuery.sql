/****************************************************************
** Name: agdSp.uspMessageSheetQuery
** Desc: 訊息傳送頁籤進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** MessageSheetType VARCHAR(1)   - 訊息傳送頁籤類別
** MessageSheetID   VARCHAR(20)  - 訊息傳送頁籤代碼
** MessageSheetName NVARCHAR(50) - 訊息傳送頁籤名稱
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
    @MessageSheetType VARCHAR(1)   - 訊息傳送頁籤類別
	@MessageSheetID  VARCHAR(20)  - 訊息傳送頁籤代碼
	@MessageSheetName NVARCHAR(50) - 訊息傳送頁籤名稱
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
	,@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @MessageSheetType = 'S'
	SET @MessageSheetID = '1234'
	SET @MessageSheetName = '1234'

EXEC @return_value = agdSp.uspMessageSheetQuery
	@MessageSheetType = @MessageSheetType
		,@MessageSheetID = @MessageSheetID
		,@MessageSheetName = @MessageSheetName
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
CREATE PROCEDURE [agdSp].[uspMessageSheetQuery] (
	@MessageSheetType VARCHAR(1)
	,@MessageSheetID VARCHAR(20)
	,@MessageSheetName NVARCHAR(50)
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
			,f.MessageSheetType
			,f.MessageSheetID
			,f.MessageSheetName
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbMessageSheet AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.MessageSheetType LIKE CASE WHEN @MessageSheetType = '' THEN f.MessageSheetType ELSE '%' + @MessageSheetType + '%' END
			AND f.MessageSheetID LIKE CASE WHEN @MessageSheetID = '' THEN f.MessageSheetID ELSE '%' + @MessageSheetID + '%' END
			AND f.MessageSheetName LIKE CASE WHEN @MessageSheetName = '' THEN f.MessageSheetName ELSE '%' + @MessageSheetName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'MessageSheetType' AND @SortOrder = 'ASC' THEN f.MessageSheetType END ASC,
			CASE WHEN @SortColumn = 'MessageSheetType' AND @SortOrder = 'DESC' THEN f.MessageSheetType END DESC,
			CASE WHEN @SortColumn = 'MessageSheetID' AND @SortOrder = 'ASC' THEN f.MessageSheetID END ASC,
			CASE WHEN @SortColumn = 'MessageSheetID' AND @SortOrder = 'DESC' THEN f.MessageSheetID END DESC,
			CASE WHEN @SortColumn = 'MessageSheetName' AND @SortOrder = 'ASC' THEN f.MessageSheetName END ASC,
			CASE WHEN @SortColumn = 'MessageSheetName' AND @SortOrder = 'DESC' THEN f.MessageSheetName END DESC,
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
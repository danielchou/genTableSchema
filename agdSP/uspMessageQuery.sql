/****************************************************************
** Name: agdSp.uspMessageQuery
** Desc: 訊息傳送進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** MessageSheetID   VARCHAR(20)  - 訊息傳送頁籤代碼
** MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
** MessageName      NVARCHAR(50) - 訊息傳送名稱
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
    @MessageSheetID  VARCHAR(20)  - 訊息傳送頁籤代碼
	@MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
	@MessageName     NVARCHAR(50) - 訊息傳送名稱
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
	,@MessageSheetID VARCHAR(20)
	,@MessageTemplateID VARCHAR(20)
	,@MessageName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @MessageSheetID = '1234'
	SET @MessageTemplateID = '1234'
	SET @MessageName = '1234'

EXEC @return_value = agdSp.uspMessageQuery
	@MessageSheetID = @MessageSheetID
		,@MessageTemplateID = @MessageTemplateID
		,@MessageName = @MessageName
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
CREATE PROCEDURE [agdSp].[uspMessageQuery] (
	@MessageSheetID VARCHAR(20)
	,@MessageTemplateID VARCHAR(20)
	,@MessageName NVARCHAR(50)
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
			,f.MessageSheetID
			,f.MessageTemplateID
			,f.MessageName
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbMessage AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.MessageSheetID LIKE CASE WHEN @MessageSheetID = '' THEN f.MessageSheetID ELSE '%' + @MessageSheetID + '%' END
			AND f.MessageTemplateID LIKE CASE WHEN @MessageTemplateID = '' THEN f.MessageTemplateID ELSE '%' + @MessageTemplateID + '%' END
			AND f.MessageName LIKE CASE WHEN @MessageName = '' THEN f.MessageName ELSE '%' + @MessageName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'MessageSheetID' AND @SortOrder = 'ASC' THEN f.MessageSheetID END ASC,
			CASE WHEN @SortColumn = 'MessageSheetID' AND @SortOrder = 'DESC' THEN f.MessageSheetID END DESC,
			CASE WHEN @SortColumn = 'MessageTemplateID' AND @SortOrder = 'ASC' THEN f.MessageTemplateID END ASC,
			CASE WHEN @SortColumn = 'MessageTemplateID' AND @SortOrder = 'DESC' THEN f.MessageTemplateID END DESC,
			CASE WHEN @SortColumn = 'MessageName' AND @SortOrder = 'ASC' THEN f.MessageName END ASC,
			CASE WHEN @SortColumn = 'MessageName' AND @SortOrder = 'DESC' THEN f.MessageName END DESC,
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
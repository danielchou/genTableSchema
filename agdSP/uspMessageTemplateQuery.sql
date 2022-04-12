/****************************************************************
** Name: agdSp.uspMessageTemplateQuery
** Desc: 訊息傳送範本進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
** MessageTemplateName NVARCHAR(50) - 訊息傳送範本名稱
** MessageB08Code   VARCHAR(20)  - 訊息傳送B08代碼
** Content          NVARCHAR(2000) - 訊息傳送範本
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
    @MessageTemplateID VARCHAR(20)  - 訊息傳送範本代碼
	@MessageTemplateName NVARCHAR(50) - 訊息傳送範本名稱
	@MessageB08Code  VARCHAR(20)  - 訊息傳送B08代碼
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
	,@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @MessageTemplateID = '1234'
	SET @MessageTemplateName = '1234'
	SET @MessageB08Code = '1234'

EXEC @return_value = agdSp.uspMessageTemplateQuery
	@MessageTemplateID = @MessageTemplateID
		,@MessageTemplateName = @MessageTemplateName
		,@MessageB08Code = @MessageB08Code
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
CREATE PROCEDURE [agdSp].[uspMessageTemplateQuery] (
	@MessageTemplateID VARCHAR(20)
	,@MessageTemplateName NVARCHAR(50)
	,@MessageB08Code VARCHAR(20)
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
			,f.MessageTemplateID
			,f.MessageTemplateName
			,f.MessageB08Code
			,f.Content
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbMessageTemplate AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.MessageTemplateID LIKE CASE WHEN @MessageTemplateID = '' THEN f.MessageTemplateID ELSE '%' + @MessageTemplateID + '%' END
			AND f.MessageTemplateName LIKE CASE WHEN @MessageTemplateName = '' THEN f.MessageTemplateName ELSE '%' + @MessageTemplateName + '%' END
			AND f.MessageB08Code LIKE CASE WHEN @MessageB08Code = '' THEN f.MessageB08Code ELSE '%' + @MessageB08Code + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'MessageTemplateID' AND @SortOrder = 'ASC' THEN f.MessageTemplateID END ASC,
			CASE WHEN @SortColumn = 'MessageTemplateID' AND @SortOrder = 'DESC' THEN f.MessageTemplateID END DESC,
			CASE WHEN @SortColumn = 'MessageTemplateName' AND @SortOrder = 'ASC' THEN f.MessageTemplateName END ASC,
			CASE WHEN @SortColumn = 'MessageTemplateName' AND @SortOrder = 'DESC' THEN f.MessageTemplateName END DESC,
			CASE WHEN @SortColumn = 'MessageB08Code' AND @SortOrder = 'ASC' THEN f.MessageB08Code END ASC,
			CASE WHEN @SortColumn = 'MessageB08Code' AND @SortOrder = 'DESC' THEN f.MessageB08Code END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
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
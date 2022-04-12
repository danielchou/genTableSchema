/****************************************************************
** Name: agdSp.uspNotificationTemplateQuery
** Desc: 通知公告範本進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** NotificationType VARCHAR(3)   - 通知公告類別
** NotificationID   VARCHAR(50)  - 通知公告代碼
** NotificationName NVARCHAR(50) - 通知公告名稱
** Content          NVARCHAR(2000) - 通知公告範本
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
    @NotificationType VARCHAR(3)   - 通知公告類別
	@NotificationID  VARCHAR(50)  - 通知公告代碼
	@NotificationName NVARCHAR(50) - 通知公告名稱
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
	,@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @NotificationType = '1'
	SET @NotificationID = '1234'
	SET @NotificationName = '1234'

EXEC @return_value = agdSp.uspNotificationTemplateQuery
	@NotificationType = @NotificationType
		,@NotificationID = @NotificationID
		,@NotificationName = @NotificationName
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
** 2022/04/12 16:52:49    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspNotificationTemplateQuery] (
	@NotificationType VARCHAR(3)
	,@NotificationID VARCHAR(50)
	,@NotificationName NVARCHAR(50)
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
			,f.NotificationType
			,f.NotificationID
			,f.NotificationName
			,f.Content
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbNotificationTemplate AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.NotificationType LIKE CASE WHEN @NotificationType = '' THEN f.NotificationType ELSE '%' + @NotificationType + '%' END
			AND f.NotificationID LIKE CASE WHEN @NotificationID = '' THEN f.NotificationID ELSE '%' + @NotificationID + '%' END
			AND f.NotificationName LIKE CASE WHEN @NotificationName = '' THEN f.NotificationName ELSE '%' + @NotificationName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'NotificationType' AND @SortOrder = 'ASC' THEN f.NotificationType END ASC,
			CASE WHEN @SortColumn = 'NotificationType' AND @SortOrder = 'DESC' THEN f.NotificationType END DESC,
			CASE WHEN @SortColumn = 'NotificationID' AND @SortOrder = 'ASC' THEN f.NotificationID END ASC,
			CASE WHEN @SortColumn = 'NotificationID' AND @SortOrder = 'DESC' THEN f.NotificationID END DESC,
			CASE WHEN @SortColumn = 'NotificationName' AND @SortOrder = 'ASC' THEN f.NotificationName END ASC,
			CASE WHEN @SortColumn = 'NotificationName' AND @SortOrder = 'DESC' THEN f.NotificationName END DESC,
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
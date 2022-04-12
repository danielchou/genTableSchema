/****************************************************************
** Name: agdSp.uspCalendarQuery
** Desc: 班表資訊進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** UserID           VARCHAR(20)  - 使用者帳號
** ScheduleDate     DATETIME2(7) - 排定日期
** Content          NVARCHAR(50) - 班表內容
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
    @UserID          VARCHAR(20)  - 使用者帳號
	@ScheduleDate    DATETIME2(7) - 排定日期
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
	,@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @UserID = '1234'
	SET @ScheduleDate = '2022-02-02 12:00:00'

EXEC @return_value = agdSp.uspCalendarQuery
	@UserID = @UserID
		,@ScheduleDate = @ScheduleDate
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
CREATE PROCEDURE [agdSp].[uspCalendarQuery] (
	@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
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
			,f.UserID
			,f.ScheduleDate
			,f.Content
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbCalendar AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.UserID LIKE CASE WHEN @UserID = '' THEN f.UserID ELSE '%' + @UserID + '%' END
			AND f.ScheduleDate LIKE CASE WHEN @ScheduleDate = '' THEN f.ScheduleDate ELSE '%' + @ScheduleDate + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'UserID' AND @SortOrder = 'ASC' THEN f.UserID END ASC,
			CASE WHEN @SortColumn = 'UserID' AND @SortOrder = 'DESC' THEN f.UserID END DESC,
			CASE WHEN @SortColumn = 'ScheduleDate' AND @SortOrder = 'ASC' THEN f.ScheduleDate END ASC,
			CASE WHEN @SortColumn = 'ScheduleDate' AND @SortOrder = 'DESC' THEN f.ScheduleDate END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
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
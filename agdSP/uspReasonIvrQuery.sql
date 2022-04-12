/****************************************************************
** Name: agdSp.uspReasonIvrQuery
** Desc: 聯繫原因Ivr配對進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** IvrID            VARCHAR(20)  - Ivr代碼
** ReasonID         VARCHAR(20)  - 原因代碼
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
    @IvrID           VARCHAR(20)  - Ivr代碼
	@ReasonID        VARCHAR(20)  - 原因代碼
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
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @IvrID = '1234'
	SET @ReasonID = '1234'

EXEC @return_value = agdSp.uspReasonIvrQuery
	@IvrID = @IvrID
		,@ReasonID = @ReasonID
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
CREATE PROCEDURE [agdSp].[uspReasonIvrQuery] (
	@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
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
			,f.IvrID
			,f.ReasonID
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbReasonIvr AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.IvrID LIKE CASE WHEN @IvrID = '' THEN f.IvrID ELSE '%' + @IvrID + '%' END
			AND f.ReasonID LIKE CASE WHEN @ReasonID = '' THEN f.ReasonID ELSE '%' + @ReasonID + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'IvrID' AND @SortOrder = 'ASC' THEN f.IvrID END ASC,
			CASE WHEN @SortColumn = 'IvrID' AND @SortOrder = 'DESC' THEN f.IvrID END DESC,
			CASE WHEN @SortColumn = 'ReasonID' AND @SortOrder = 'ASC' THEN f.ReasonID END ASC,
			CASE WHEN @SortColumn = 'ReasonID' AND @SortOrder = 'DESC' THEN f.ReasonID END DESC,
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
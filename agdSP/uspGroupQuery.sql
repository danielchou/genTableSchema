/****************************************************************
** Name: agdSp.uspGroupQuery
** Desc: 群組進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** GroupID          VARCHAR(20)  - 群組代碼
** GroupName        NVARCHAR(50) - 群組名稱
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
    @GroupID         VARCHAR(20)  - 群組代碼
	@GroupName       NVARCHAR(50) - 群組名稱
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
	,@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @GroupID = '1'
	SET @GroupName = 'aaa'

EXEC @return_value = agdSp.uspGroupQuery
	@GroupID = @GroupID
		,@GroupName = @GroupName
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
** 2022/04/12 16:52:47    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupQuery] (
	@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
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
			,f.GroupID
			,f.GroupName
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbGroup AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.GroupID LIKE CASE WHEN @GroupID = '' THEN f.GroupID ELSE '%' + @GroupID + '%' END
			AND f.GroupName LIKE CASE WHEN @GroupName = '' THEN f.GroupName ELSE '%' + @GroupName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'GroupID' AND @SortOrder = 'ASC' THEN f.GroupID END ASC,
			CASE WHEN @SortColumn = 'GroupID' AND @SortOrder = 'DESC' THEN f.GroupID END DESC,
			CASE WHEN @SortColumn = 'GroupName' AND @SortOrder = 'ASC' THEN f.GroupName END ASC,
			CASE WHEN @SortColumn = 'GroupName' AND @SortOrder = 'DESC' THEN f.GroupName END DESC,
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
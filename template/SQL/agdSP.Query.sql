/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_query]
** Desc: $pt_tbDscr進階查詢
**
** Return values: 0 成功
** Return Recordset: 
$pt_getSelectAll
** Total            INT          - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
  $pt_inputQuery
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
	,$pt_DeclareQuery
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	$pt_querySetVal

EXEC @return_value = [agdSp].[usp$pt_tableName$pt_query]
	$pt_Exec
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
** $pt_DateTime    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp$pt_tableName$pt_query] (
	$pt_DeclareQuery
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
  ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
      $pt_fColAll
			,COUNT(1) OVER () AS Total
		FROM $pt_schema.tb$pt_tableName AS f
		------- WHERE 查詢條件 -------
		WHERE $pt_queryWhere
		------- Sort 排序條件 -------
		ORDER BY 
$pt_orderBy
      CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
			CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_insert]
** Desc: $pt_tbDscr新增
**/
DECLARE @return_value int
	,$pt_DeclareInsert
	,@ErrorMsg NVARCHAR(100);

	$pt_insertSetVal

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_insert] 
	$pt_ExecInsert
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT '$pt_tbDscr新增' as title
	,@return_value AS 'Return Value'
	,@ErrorMsg AS N'@ErrorMsg'
GO


/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_get]
** Desc: $pt_tbDscr查詢
**/
	DECLARE @return_value INT
	,$pt_DeclareGet
	,@ErrorMsg NVARCHAR(100)

	$pt_getSetValue

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_get]
		$pt_ExecGet
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT '$pt_tbDscr查詢' as title
		,@return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
GO


/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_update]
** Desc: $pt_tbDscr更新
**/

DECLARE @return_value INT
  ,$pt_DeclareUpdate
  ,@ErrorMsg NVARCHAR(100)

  $pt_updateSetVal

EXEC @return_value = [agdSp].[usp$pt_tableName$pt_update]
    $pt_ExecUpdate
	,@ErrorMsg = @ErrorMsg OUTPUT
SELECT '$pt_tbDscr更新' as title
		,@return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
GO

/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_query]
** Desc: $pt_tbDscr進階Query查詢
**/
DECLARE @return_value INT
	,$pt_DeclareQuery
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	$pt_querySetVal

EXEC @return_value = [agdSp].[usp$pt_tableName$pt_query]
	$pt_ExecQuery
	,@Page = @Page
	,@RowsPerPage = @RowsPerPage
	,@SortColumn = @SortColumn
	,@SortOrder = @SortOrder
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT '$pt_tbDscr進階Query查詢' as title
		,@return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
GO


/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_delete]
** Desc: $pt_tbDscr刪除
**/
	DECLARE @return_value INT
		,$pt_DeclarePK
		,@ErrorMsg NVARCHAR(100)

	$pt_deleteSetVal

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_delete] 
		$pt_cmmtExecPK
		,@ErrorMsg = @ErrorMsg OUTPUT

SELECT '$pt_tbDscr刪除' as title
		,@return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
GO

/****************************************************************
** Name: [agdSp].[usp$pt_tableName$pt_get]
** Desc: $pt_tbDscr查詢
**/
	DECLARE @return_value INT
	,$pt_DeclareGet
	,@ErrorMsg NVARCHAR(100)

	$pt_getSetValue

	EXEC @return_value = [agdSp].[usp$pt_tableName$pt_get]
		$pt_ExecGet
		,@ErrorMsg = @ErrorMsg OUTPUT

SELECT '$pt_tbDscr查詢' as title
		,@return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
GO
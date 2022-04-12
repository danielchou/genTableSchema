/****************************************************************
** Name: agdSp.uspFuncQuery
** Desc: 功能進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** FuncID           VARCHAR(20)  - 功能代碼
** FuncName         NVARCHAR(50) - 功能名稱
** ParentFuncID     VARCHAR(20)  - 上層功能代碼
** Level            TINYINT      - 階層
** SystemType       VARCHAR(20)  - 系統類別
** IconName         VARCHAR(20)  - Icon名稱
** RouteName        VARCHAR(50)  - 路由名稱
** DisplayOrder     INT          - 顯示順序
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
    @FuncID          VARCHAR(20)  - 功能代碼
	@FuncName        NVARCHAR(50) - 功能名稱
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
	,@FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @FuncID = 'func'
	SET @FuncName = 'aaa'

EXEC @return_value = agdSp.uspFuncQuery
	@FuncID = @FuncID
		,@FuncName = @FuncName
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
CREATE PROCEDURE [agdSp].[uspFuncQuery] (
	@FuncID VARCHAR(20)
	,@FuncName NVARCHAR(50)
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
			,f.FuncID
			,f.FuncName
			,f.ParentFuncID
			,f.Level
			,f.SystemType
			,f.IconName
			,f.RouteName
			,f.DisplayOrder
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbFunc AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.FuncID LIKE CASE WHEN @FuncID = '' THEN f.FuncID ELSE '%' + @FuncID + '%' END
			AND f.FuncName LIKE CASE WHEN @FuncName = '' THEN f.FuncName ELSE '%' + @FuncName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'FuncID' AND @SortOrder = 'ASC' THEN f.FuncID END ASC,
			CASE WHEN @SortColumn = 'FuncID' AND @SortOrder = 'DESC' THEN f.FuncID END DESC,
			CASE WHEN @SortColumn = 'FuncName' AND @SortOrder = 'ASC' THEN f.FuncName END ASC,
			CASE WHEN @SortColumn = 'FuncName' AND @SortOrder = 'DESC' THEN f.FuncName END DESC,
			CASE WHEN @SortColumn = 'SystemType' AND @SortOrder = 'ASC' THEN f.SystemType END ASC,
			CASE WHEN @SortColumn = 'SystemType' AND @SortOrder = 'DESC' THEN f.SystemType END DESC,
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
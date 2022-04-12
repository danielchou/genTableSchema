/****************************************************************
** Name: agdSp.uspSysConfigQuery
** Desc: 系統參數進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** SysConfigType    VARCHAR(20)  - 系統參數類別
** SysConfigID      VARCHAR(20)  - 系統參數代碼
** SysConfigName    NVARCHAR(50) - 系統參數名稱
** Content          NVARCHAR(200) - 系統參數內容
** IsVisible        BIT          - 是否顯示?
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
    @SysConfigType   VARCHAR(20)  - 系統參數類別
	@SysConfigID     VARCHAR(20)  - 系統參數代碼
	@SysConfigName   NVARCHAR(50) - 系統參數名稱
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
	,@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @SysConfigType = '1234'
	SET @SysConfigID = '1234'
	SET @SysConfigName = '1234'

EXEC @return_value = agdSp.uspSysConfigQuery
	@SysConfigType = @SysConfigType
		,@SysConfigID = @SysConfigID
		,@SysConfigName = @SysConfigName
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
** 2022/04/12 16:52:51    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspSysConfigQuery] (
	@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
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
			,f.SysConfigType
			,f.SysConfigID
			,f.SysConfigName
			,f.Content
			,f.IsVisible
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbSysConfig AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.SysConfigType LIKE CASE WHEN @SysConfigType = '' THEN f.SysConfigType ELSE '%' + @SysConfigType + '%' END
			AND f.SysConfigID LIKE CASE WHEN @SysConfigID = '' THEN f.SysConfigID ELSE '%' + @SysConfigID + '%' END
			AND f.SysConfigName LIKE CASE WHEN @SysConfigName = '' THEN f.SysConfigName ELSE '%' + @SysConfigName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'SysConfigType' AND @SortOrder = 'ASC' THEN f.SysConfigType END ASC,
			CASE WHEN @SortColumn = 'SysConfigType' AND @SortOrder = 'DESC' THEN f.SysConfigType END DESC,
			CASE WHEN @SortColumn = 'SysConfigID' AND @SortOrder = 'ASC' THEN f.SysConfigID END ASC,
			CASE WHEN @SortColumn = 'SysConfigID' AND @SortOrder = 'DESC' THEN f.SysConfigID END DESC,
			CASE WHEN @SortColumn = 'SysConfigName' AND @SortOrder = 'ASC' THEN f.SysConfigName END ASC,
			CASE WHEN @SortColumn = 'SysConfigName' AND @SortOrder = 'DESC' THEN f.SysConfigName END DESC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'ASC' THEN f.Content END ASC,
			CASE WHEN @SortColumn = 'Content' AND @SortOrder = 'DESC' THEN f.Content END DESC,
			CASE WHEN @SortColumn = 'IsVisible' AND @SortOrder = 'ASC' THEN f.IsVisible END ASC,
			CASE WHEN @SortColumn = 'IsVisible' AND @SortOrder = 'DESC' THEN f.IsVisible END DESC,
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
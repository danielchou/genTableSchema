/****************************************************************
** Name: agdSp.uspAuxCodeQuery
** Desc: 休息碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** AuxID            VARCHAR(20)  - 休息碼代碼
** AuxName          NVARCHAR(50) - 休息碼名稱
** IsLongTimeAux    BIT          - 是否長時間離開?
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
    @AuxID           VARCHAR(20)  - 休息碼代碼
	@AuxName         NVARCHAR(50) - 休息碼名稱
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
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @AuxID = '1234'
	SET @AuxName = '1234'

EXEC @return_value = agdSp.uspAuxCodeQuery
	@AuxID = @AuxID
		,@AuxName = @AuxName
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
CREATE PROCEDURE [agdSp].[uspAuxCodeQuery] (
	@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
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
			,f.AuxID
			,f.AuxName
			,f.IsLongTimeAux
			,f.DisplayOrder
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbAuxCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.AuxID LIKE CASE WHEN @AuxID = '' THEN f.AuxID ELSE '%' + @AuxID + '%' END
			AND f.AuxName LIKE CASE WHEN @AuxName = '' THEN f.AuxName ELSE '%' + @AuxName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'AuxID' AND @SortOrder = 'ASC' THEN f.AuxID END ASC,
			CASE WHEN @SortColumn = 'AuxID' AND @SortOrder = 'DESC' THEN f.AuxID END DESC,
			CASE WHEN @SortColumn = 'AuxName' AND @SortOrder = 'ASC' THEN f.AuxName END ASC,
			CASE WHEN @SortColumn = 'AuxName' AND @SortOrder = 'DESC' THEN f.AuxName END DESC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'ASC' THEN f.DisplayOrder END ASC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'DESC' THEN f.DisplayOrder END DESC,
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
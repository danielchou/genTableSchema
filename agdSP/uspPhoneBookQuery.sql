/****************************************************************
** Name: agdSp.uspPhoneBookQuery
** Desc: 電話簿進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** PhoneBookID      VARCHAR(20)  - 電話簿代碼
** PhoneBookName    NVARCHAR(50) - 電話簿名稱
** ParentPhoneBookID VARCHAR(20)  - 上層電話簿代碼
** PhoneBookNumber  VARCHAR(20)  - 電話號碼
** Level            TINYINT      - 階層
** Memo             NVARCHAR(200) - 備註
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
    @PhoneBookID     VARCHAR(20)  - 電話簿代碼
	@PhoneBookName   NVARCHAR(50) - 電話簿名稱
	@ParentPhoneBookID VARCHAR(20)  - 上層電話簿代碼
	@PhoneBookNumber VARCHAR(20)  - 電話號碼
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
	,@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
	,@ParentPhoneBookID VARCHAR(20)
	,@PhoneBookNumber VARCHAR(20)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @PhoneBookID = '1234'
	SET @PhoneBookName = '1234'
	SET @ParentPhoneBookID = '1234'
	SET @PhoneBookNumber = '1234'

EXEC @return_value = agdSp.uspPhoneBookQuery
	@PhoneBookID = @PhoneBookID
		,@PhoneBookName = @PhoneBookName
		,@ParentPhoneBookID = @ParentPhoneBookID
		,@PhoneBookNumber = @PhoneBookNumber
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
CREATE PROCEDURE [agdSp].[uspPhoneBookQuery] (
	@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
	,@ParentPhoneBookID VARCHAR(20)
	,@PhoneBookNumber VARCHAR(20)
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
			,f.PhoneBookID
			,f.PhoneBookName
			,f.ParentPhoneBookID
			,f.PhoneBookNumber
			,f.Level
			,f.Memo
			,f.DisplayOrder
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbPhoneBook AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.PhoneBookID LIKE CASE WHEN @PhoneBookID = '' THEN f.PhoneBookID ELSE '%' + @PhoneBookID + '%' END
			AND f.PhoneBookName LIKE CASE WHEN @PhoneBookName = '' THEN f.PhoneBookName ELSE '%' + @PhoneBookName + '%' END
			AND f.ParentPhoneBookID LIKE CASE WHEN @ParentPhoneBookID = '' THEN f.ParentPhoneBookID ELSE '%' + @ParentPhoneBookID + '%' END
			AND f.PhoneBookNumber LIKE CASE WHEN @PhoneBookNumber = '' THEN f.PhoneBookNumber ELSE '%' + @PhoneBookNumber + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'PhoneBookID' AND @SortOrder = 'ASC' THEN f.PhoneBookID END ASC,
			CASE WHEN @SortColumn = 'PhoneBookID' AND @SortOrder = 'DESC' THEN f.PhoneBookID END DESC,
			CASE WHEN @SortColumn = 'PhoneBookName' AND @SortOrder = 'ASC' THEN f.PhoneBookName END ASC,
			CASE WHEN @SortColumn = 'PhoneBookName' AND @SortOrder = 'DESC' THEN f.PhoneBookName END DESC,
			CASE WHEN @SortColumn = 'ParentPhoneBookID' AND @SortOrder = 'ASC' THEN f.ParentPhoneBookID END ASC,
			CASE WHEN @SortColumn = 'ParentPhoneBookID' AND @SortOrder = 'DESC' THEN f.ParentPhoneBookID END DESC,
			CASE WHEN @SortColumn = 'PhoneBookNumber' AND @SortOrder = 'ASC' THEN f.PhoneBookNumber END ASC,
			CASE WHEN @SortColumn = 'PhoneBookNumber' AND @SortOrder = 'DESC' THEN f.PhoneBookNumber END DESC,
			CASE WHEN @SortColumn = 'Level' AND @SortOrder = 'ASC' THEN f.Level END ASC,
			CASE WHEN @SortColumn = 'Level' AND @SortOrder = 'DESC' THEN f.Level END DESC,
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
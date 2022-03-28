/****************************************************************
** Name: agdSp.uspCodeQuery
** Desc: 系統代碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  codeType	 - 代碼分類
**  codeId	 - 系統代碼檔代碼
**  codeName	 - 系統代碼檔名稱
**  isEnable	 - 是否啟用?
**  creator	 - 建立者
**  updator	 - 異動者
**  createDT	 - 建立時間
**  updateDT	 - 異動時間
**	UpdatorName - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    
	@Page INT - 頁數
	@RowsPerPage INT - 每頁筆數
	@SortColumn NVARCHAR(30) - 排序欄位
	@SortOrder VARCHAR(10) - 排序順序
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
	,
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	

EXEC @return_value = agdSp.uspCodeQuery
	
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
** 2022-03-28 14:45:45    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeQuery] (
	
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
            
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.codeType LIKE CASE WHEN @codeType = '' THEN f.codeType ELSE '%' + @codeType + '%' END
				AND f.codeId LIKE CASE WHEN @codeId = '' THEN f.codeId ELSE '%' + @codeId + '%' END
				AND f.codeName LIKE CASE WHEN @codeName = '' THEN f.codeName ELSE '%' + @codeName + '%' END
				AND f.isEnable LIKE CASE WHEN @isEnable = '' THEN f.isEnable ELSE '%' + @isEnable + '%' END
				AND f.creator LIKE CASE WHEN @creator = '' THEN f.creator ELSE '%' + @creator + '%' END
				AND f.updator LIKE CASE WHEN @updator = '' THEN f.updator ELSE '%' + @updator + '%' END
				AND f.createDT LIKE CASE WHEN @createDT = '' THEN f.createDT ELSE '%' + @createDT + '%' END
				AND f.updateDT LIKE CASE WHEN @updateDT = '' THEN f.updateDT ELSE '%' + @updateDT + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'codeType' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'codeType' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'codeId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'codeId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'codeName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'codeName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'creator' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'creator' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
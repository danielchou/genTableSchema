/****************************************************************
** Name: agdSp.uspPcPhoneQuery
** Desc: 電腦電話進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  extCode	 - 分機號碼
**  computerName	 - 電腦名稱
**  computerIP	 - 電腦IP
**  memo	 - 備註
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

	

EXEC @return_value = agdSp.uspPcPhoneQuery
	
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
** 2022-03-28 14:45:46    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneQuery] (
	
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
		FROM agdSet.tbPcPhone AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.extCode LIKE CASE WHEN @extCode = '' THEN f.extCode ELSE '%' + @extCode + '%' END
				AND f.computerName LIKE CASE WHEN @computerName = '' THEN f.computerName ELSE '%' + @computerName + '%' END
				AND f.computerIP LIKE CASE WHEN @computerIP = '' THEN f.computerIP ELSE '%' + @computerIP + '%' END
				AND f.memo LIKE CASE WHEN @memo = '' THEN f.memo ELSE '%' + @memo + '%' END
				AND f.isEnable LIKE CASE WHEN @isEnable = '' THEN f.isEnable ELSE '%' + @isEnable + '%' END
				AND f.creator LIKE CASE WHEN @creator = '' THEN f.creator ELSE '%' + @creator + '%' END
				AND f.updator LIKE CASE WHEN @updator = '' THEN f.updator ELSE '%' + @updator + '%' END
				AND f.createDT LIKE CASE WHEN @createDT = '' THEN f.createDT ELSE '%' + @createDT + '%' END
				AND f.updateDT LIKE CASE WHEN @updateDT = '' THEN f.updateDT ELSE '%' + @updateDT + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'extCode' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'extCode' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'computerName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'computerName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'computerIP' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'computerIP' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'memo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'memo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'updateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'updateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
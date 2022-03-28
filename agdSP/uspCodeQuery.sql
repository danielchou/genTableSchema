/****************************************************************
** Name: agdSp.uspCodeQuery
** Desc: 系統代碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  CodeType	 - 代碼分類
**  CodeId	 - 系統代碼檔代碼
**  CodeName	 - 系統代碼檔名稱
**  IsEnable	 - 是否啟用?
**  Creator	 - 建立者
**  Updator	 - 異動者
**  CreateDT	 - 建立時間
**  UpdateDT	 - 異動時間
**	UpdatorName - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @CodeType	NVARCHAR(20) - 代碼分類
	@CodeId	VARCHAR(20) - 系統代碼檔代碼
	@CodeName	NVARCHAR(50) - 系統代碼檔名稱
	@IsEnable	BIT - 是否啟用?
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
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'
	SET @IsEnable = 1

EXEC @return_value = agdSp.uspCodeQuery
	@CodeType = @CodeType
	,@CodeId = @CodeId
	,@CodeName = @CodeName
	,@IsEnable = @IsEnable
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
** 2022-03-28 12:29:50    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeQuery] (
	@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@IsEnable BIT
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
            f.CodeType
			,f.CodeId
			,f.CodeName
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.CodeType LIKE CASE WHEN @CodeType = '' THEN f.CodeType ELSE '%' + @CodeType + '%' END
				AND f.CodeId LIKE CASE WHEN @CodeId = '' THEN f.CodeId ELSE '%' + @CodeId + '%' END
				AND f.CodeName LIKE CASE WHEN @CodeName = '' THEN f.CodeName ELSE '%' + @CodeName + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'CodeType' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CodeType' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CodeId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CodeId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CodeName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CodeName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'Creator' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'Creator' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
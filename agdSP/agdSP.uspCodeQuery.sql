/****************************************************************
** Name: agdSp.uspCodeQuery
** Desc: 系統代碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
    @SeqNo INT	-	代碼序號
	@CodeType NVARCHAR(20)	-	代碼類型
	@CodeId VARCHAR(20)	-	代碼
	@CodeName NVARCHAR(20)	-	代碼名稱
**	UpdatorName NVARCHAR(20) - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @SeqNo INT	-	代碼序號
	@CodeType NVARCHAR(20)	-	代碼類型
	@CodeId VARCHAR(20)	-	代碼
	@CodeName NVARCHAR(20)	-	代碼名稱
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
        @SeqNo INT
		,@CodeType NVARCHAR(20)
		,@CodeId VARCHAR(20)
		,@CodeName NVARCHAR(20)
        ,@Page INT = 1
        ,@RowsPerPage INT = 20
        ,@SortColumn NVARCHAR(30) = 'CreateDT'
        ,@SortOrder VARCHAR(10) = 'ASC'
        ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = ''
	SET @CodeType = 'sys'
	SET @CodeId = 1
	SET @CodeName = 'admin'

	EXEC @return_value = agdSp.uspCodeQuery
        @SeqNo = @SeqNo
		,@CodeType = @CodeType
		,@CodeId = @CodeId
		,@CodeName = @CodeName
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
** 2022-03-18 00:27:00    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspCodeQuery] (
        @SeqNo INT
		,@CodeType NVARCHAR(20)
		,@CodeId VARCHAR(20)
		,@CodeName NVARCHAR(20)
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
            f.SeqNo
			,f.CodeType
			,f.CodeId
			,f.CodeName
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE 
            f.GroupId LIKE CASE WHEN @GroupId = '' THEN f.GroupId ELSE '%' + @GroupId + '%' END
		AND f.GroupName LIKE CASE WHEN @GroupName = '' THEN f.GroupName ELSE '%' + @GroupName + '%' END
		AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
            CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
			CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
            CASE WHEN @SortColumn = 'CodeId' AND @SortOrder = 'ASC' THEN f.CodeId END ASC,
            CASE WHEN @SortColumn = 'CodeId' AND @SortOrder = 'DESC' THEN f.CodeId END DESC,
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

/****************************************************************
** Name: agdSp.uspUserQuery
** Desc: 使用者進階查詢
**
** Return values: 0 成功
** Return Recordset: 
    @SeqNo INT	-	使用者序號
	@UserId NVARCHAR(20)	-	使用者帳號
	@UserName NVARCHAR(50)	-	使用者名稱
**	UpdatorName NVARCHAR(20) - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @SeqNo INT	-	使用者序號
	@UserId NVARCHAR(20)	-	使用者帳號
	@UserName NVARCHAR(50)	-	使用者名稱
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
	,@UserId NVARCHAR(20)
	,@UserName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

SET @SeqNo = 2
	SET @UserId = 'agent'
	SET @UserName = 'BBB'

EXEC @return_value = agdSp.uspUserQuery
	@SeqNo = @SeqNo
	,@UserId = @UserId
	,@UserName = @UserName
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
** 2022-03-22 00:40:38    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspUserQuery] (
	@SeqNo INT
	,@UserId NVARCHAR(20)
	,@UserName NVARCHAR(50)
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
			,f.UserId
			,f.UserName
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbUser AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.UserId LIKE CASE WHEN @UserId = '' THEN f.UserId ELSE '%' + @UserId + '%' END
				AND f.UserName LIKE CASE WHEN @UserName = '' THEN f.UserName ELSE '%' + @UserName + '%' END
				AND f.AgentId LIKE CASE WHEN @AgentId = '' THEN f.AgentId ELSE '%' + @AgentId + '%' END
				AND f.DeptId LIKE CASE WHEN @DeptId = '' THEN f.DeptId ELSE '%' + @DeptId + '%' END
				AND f.ExtPhone LIKE CASE WHEN @ExtPhone = '' THEN f.ExtPhone ELSE '%' + @ExtPhone + '%' END
				AND f.MobilePhone LIKE CASE WHEN @MobilePhone = '' THEN f.MobilePhone ELSE '%' + @MobilePhone + '%' END
				AND f.Email LIKE CASE WHEN @Email = '' THEN f.Email ELSE '%' + @Email + '%' END
				AND f.IsAdmin LIKE CASE WHEN @IsAdmin = '' THEN f.IsAdmin ELSE '%' + @IsAdmin + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'UserId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'UserId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'Password' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'Password' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'DeptId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'DeptId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ExtPhone' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtPhone' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'MobilePhone' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'MobilePhone' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
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

/****************************************************************
** Name: agdSp.uspUserQuery
** Desc: 使用者進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  UserId	 - 使用者ID
**  Password	 - 密碼
**  UserName	 - 使用者名稱
**  AgentId	 - 經辦代碼
**  GroupId	 - 部門代碼
**  ExtPhone	 - 分機號碼
**  MobilePhone	 - 手機號碼
**  Email	 - EMAIL
**  IsAdmin	 - 是否為主管
**  IsEnable	 - 是否啟用?
**  CreateDT	 - 建立時間
**  Creator	 - 建立者
**  Updator	 - 異動者
**  CreateDt	 - 建立時間
**  UpdateDt	 - 異動時間
**	UpdatorName - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @UserId	VARCHAR(20) - 使用者ID
	@UserName	NVARCHAR(50) - 使用者名稱
	@AgentId	NVARCHAR(20) - 經辦代碼
	@GroupId	NVARCHAR(20) - 部門代碼
	@ExtPhone	NVARCHAR(10) - 分機號碼
	@MobilePhone	NVARCHAR(20) - 手機號碼
	@Email	VARCHAR(50) - EMAIL
	@IsAdmin	BIT - 是否為主管
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
	,@UserId VARCHAR(20)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@GroupId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @UserId = 'A02344'
	SET @UserName = 'daniel'
	SET @AgentId = '0033'
	SET @GroupId = 'A02'
	SET @ExtPhone = '334'
	SET @MobilePhone = '0930744573'
	SET @Email = 'daniel@esun.bank.com'
	SET @IsAdmin = 1
	SET @IsEnable = 1

EXEC @return_value = agdSp.uspUserQuery
	@UserId = @UserId
	,@UserName = @UserName
	,@AgentId = @AgentId
	,@GroupId = @GroupId
	,@ExtPhone = @ExtPhone
	,@MobilePhone = @MobilePhone
	,@Email = @Email
	,@IsAdmin = @IsAdmin
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
** 2022-04-01 13:51:26    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserQuery] (
	@UserId VARCHAR(20)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@GroupId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
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
            f.UserId
			,f.UserName
			,f.AgentId
			,f.GroupId
			,f.ExtPhone
			,f.MobilePhone
			,f.Email
			,f.IsAdmin
			,f.IsEnable
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
				AND f.Password LIKE CASE WHEN @Password = '' THEN f.Password ELSE '%' + @Password + '%' END
				AND f.UserName LIKE CASE WHEN @UserName = '' THEN f.UserName ELSE '%' + @UserName + '%' END
				AND f.AgentId LIKE CASE WHEN @AgentId = '' THEN f.AgentId ELSE '%' + @AgentId + '%' END
				AND f.GroupId LIKE CASE WHEN @GroupId = '' THEN f.GroupId ELSE '%' + @GroupId + '%' END
				AND f.ExtPhone LIKE CASE WHEN @ExtPhone = '' THEN f.ExtPhone ELSE '%' + @ExtPhone + '%' END
				AND f.MobilePhone LIKE CASE WHEN @MobilePhone = '' THEN f.MobilePhone ELSE '%' + @MobilePhone + '%' END
				AND f.Email LIKE CASE WHEN @Email = '' THEN f.Email ELSE '%' + @Email + '%' END
				AND f.IsAdmin LIKE CASE WHEN @IsAdmin = '' THEN f.IsAdmin ELSE '%' + @IsAdmin + '%' END
				AND f.CreateDt LIKE CASE WHEN @CreateDt = '' THEN f.CreateDt ELSE '%' + @CreateDt + '%' END
				AND f.UpdateDt LIKE CASE WHEN @UpdateDt = '' THEN f.UpdateDt ELSE '%' + @UpdateDt + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'UserId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'UserId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'Password' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'Password' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'GroupId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'GroupId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ExtPhone' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtPhone' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'MobilePhone' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'MobilePhone' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CreateDt' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CreateDt' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
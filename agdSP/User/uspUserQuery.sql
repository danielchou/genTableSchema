/****************************************************************
** Name: agdSp.uspUserQuery
** Desc: 使用者進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** UserID           VARCHAR(20)  - 使用者帳號
** UserName         NVARCHAR(60) - 使用者名稱
** UserCode         VARCHAR(50)  - 使用者代碼
** AgentLoginID     VARCHAR(20)  - CTI登入帳號
** AgentLoginCode   VARCHAR(20)  - CTI登入代碼
** EmployeeNo       VARCHAR(11)  - 員工編號
** NickName         NVARCHAR(50) - 使用者暱稱
** EmpDept          VARCHAR(20)  - 所屬單位
** GroupID          VARCHAR(20)  - 群組代碼
** OfficeEmail      VARCHAR(70)  - 公司Email
** EmployedStatusCode VARCHAR(1)   - 在職狀態代碼
** IsSupervisor     BIT          - 是否為主管?
** B08Code1         NVARCHAR(100) - B08_code1
** B08Code2         NVARCHAR(100) - B08_code2
** B08Code3         NVARCHAR(100) - B08_code3
** B08Code4         NVARCHAR(100) - B08_code4
** B08Code5         NVARCHAR(100) - B08_code5
** IsEnable         BIT          - 是否啟用?
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
    @UserID          VARCHAR(20)  - 使用者帳號
	@UserName        NVARCHAR(60) - 使用者名稱
	@AgentLoginID    VARCHAR(20)  - CTI登入帳號
	@GroupID         VARCHAR(20)  - 群組代碼
	@IsEnable        VARCHAR(10)  - 是否啟用?
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
	,@UserID VARCHAR(20)
	,@UserName NVARCHAR(60)
	,@AgentLoginID VARCHAR(20)
	,@GroupID VARCHAR(20)
	,@IsEnable VARCHAR(10)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @UserID = ''
	SET @UserName = ''
	SET @AgentLoginID = ''
	SET @GroupID = ''
	SET @IsEnable = ''

EXEC @return_value = agdSp.uspUserQuery
	@UserID = @UserID
		,@UserName = @UserName
		,@AgentLoginID = @AgentLoginID
		,@GroupID = @GroupID
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
** 2022/04/11 14:11:34    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserQuery] (
	@UserID VARCHAR(20)
	,@UserName NVARCHAR(60)
	,@AgentLoginID VARCHAR(20)
	,@GroupID VARCHAR(20)
	,@IsEnable VARCHAR(10)
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
			,f.UserID
			,f.UserName
			,f.UserCode
			,f.AgentLoginID
			,f.AgentLoginCode
			,f.EmployeeNo
			,f.NickName
			,f.EmpDept
			,f.GroupID
			,f.OfficeEmail
			,f.EmployedStatusCode
			,f.IsSupervisor
			,f.B08Code1
			,f.B08Code2
			,f.B08Code3
			,f.B08Code4
			,f.B08Code5
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
		WHERE  f.UserID LIKE CASE WHEN @UserID = '' THEN f.UserID ELSE '%' + @UserID + '%' END
				AND f.UserName LIKE CASE WHEN @UserName = '' THEN f.UserName ELSE '%' + @UserName + '%' END
				AND f.AgentLoginID LIKE CASE WHEN @AgentLoginID = '' THEN f.AgentLoginID ELSE '%' + @AgentLoginID + '%' END
				AND f.GroupID LIKE CASE WHEN @GroupID = '' THEN f.GroupID ELSE '%' + @GroupID + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'UserID' AND @SortOrder = 'ASC' THEN f.UserID END ASC,
				CASE WHEN @SortColumn = 'UserID' AND @SortOrder = 'DESC' THEN f.UserID END DESC,
				CASE WHEN @SortColumn = 'UserName' AND @SortOrder = 'ASC' THEN f.UserName END ASC,
				CASE WHEN @SortColumn = 'UserName' AND @SortOrder = 'DESC' THEN f.UserName END DESC,
				CASE WHEN @SortColumn = 'AgentLoginID' AND @SortOrder = 'ASC' THEN f.AgentLoginID END ASC,
				CASE WHEN @SortColumn = 'AgentLoginID' AND @SortOrder = 'DESC' THEN f.AgentLoginID END DESC,
				CASE WHEN @SortColumn = 'EmpDept' AND @SortOrder = 'ASC' THEN f.EmpDept END ASC,
				CASE WHEN @SortColumn = 'EmpDept' AND @SortOrder = 'DESC' THEN f.EmpDept END DESC,
				CASE WHEN @SortColumn = 'OfficeEmail' AND @SortOrder = 'ASC' THEN f.OfficeEmail END ASC,
				CASE WHEN @SortColumn = 'OfficeEmail' AND @SortOrder = 'DESC' THEN f.OfficeEmail END DESC,
				CASE WHEN @SortColumn = 'EmployedStatusCode' AND @SortOrder = 'ASC' THEN f.EmployedStatusCode END ASC,
				CASE WHEN @SortColumn = 'EmployedStatusCode' AND @SortOrder = 'DESC' THEN f.EmployedStatusCode END DESC,
				CASE WHEN @SortColumn = 'B08Code1' AND @SortOrder = 'ASC' THEN f.B08Code1 END ASC,
				CASE WHEN @SortColumn = 'B08Code1' AND @SortOrder = 'DESC' THEN f.B08Code1 END DESC,
				CASE WHEN @SortColumn = 'B08Code2' AND @SortOrder = 'ASC' THEN f.B08Code2 END ASC,
				CASE WHEN @SortColumn = 'B08Code2' AND @SortOrder = 'DESC' THEN f.B08Code2 END DESC,
				CASE WHEN @SortColumn = 'B08Code3' AND @SortOrder = 'ASC' THEN f.B08Code3 END ASC,
				CASE WHEN @SortColumn = 'B08Code3' AND @SortOrder = 'DESC' THEN f.B08Code3 END DESC,
				CASE WHEN @SortColumn = 'B08Code4' AND @SortOrder = 'ASC' THEN f.B08Code4 END ASC,
				CASE WHEN @SortColumn = 'B08Code4' AND @SortOrder = 'DESC' THEN f.B08Code4 END DESC,
				CASE WHEN @SortColumn = 'B08Code5' AND @SortOrder = 'ASC' THEN f.B08Code5 END ASC,
				CASE WHEN @SortColumn = 'B08Code5' AND @SortOrder = 'DESC' THEN f.B08Code5 END DESC,
				CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'ASC' THEN f.IsEnable END ASC,
				CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'DESC' THEN f.IsEnable END DESC,
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
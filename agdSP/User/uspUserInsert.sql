/****************************************************************
** Name: [agdSp].[uspUserInsert]
** Desc: 使用者新增
**
** Return values: 0 成功
** Return Recordset: 
**	NA
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@UserID          VARCHAR(20)  - 使用者帳號
	@UserName        NVARCHAR(60) - 使用者名稱
	@UserCode        VARCHAR(50)  - 使用者代碼
	@AgentLoginID    VARCHAR(20)  - CTI登入帳號
	@AgentLoginCode  VARCHAR(20)  - CTI登入代碼
	@EmployeeNo      VARCHAR(11)  - 員工編號
	@NickName        NVARCHAR(50) - 使用者暱稱
	@EmpDept         VARCHAR(20)  - 所屬單位
	@GroupID         VARCHAR(20)  - 群組代碼
	@OfficeEmail     VARCHAR(70)  - 公司Email
	@EmployedStatusCode VARCHAR(1)   - 在職狀態代碼
	@IsSupervisor    BIT          - 是否為主管?
	@B08Code1        NVARCHAR(100) - B08_code1
	@B08Code2        NVARCHAR(100) - B08_code2
	@B08Code3        NVARCHAR(100) - B08_code3
	@B08Code4        NVARCHAR(100) - B08_code4
	@B08Code5        NVARCHAR(100) - B08_code5
	@IsEnable        BIT          - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
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
	,@UserCode VARCHAR(50)
	,@AgentLoginID VARCHAR(20)
	,@AgentLoginCode VARCHAR(20)
	,@EmployeeNo VARCHAR(11)
	,@NickName NVARCHAR(50)
	,@EmpDept VARCHAR(20)
	,@GroupID VARCHAR(20)
	,@OfficeEmail VARCHAR(70)
	,@EmployedStatusCode VARCHAR(1)
	,@IsSupervisor BIT
	,@B08Code1 NVARCHAR(100)
	,@B08Code2 NVARCHAR(100)
	,@B08Code3 NVARCHAR(100)
	,@B08Code4 NVARCHAR(100)
	,@B08Code5 NVARCHAR(100)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @UserID = ''
	SET @UserName = ''
	SET @UserCode = ''
	SET @AgentLoginID = ''
	SET @AgentLoginCode = ''
	SET @EmployeeNo = ''
	SET @NickName = ''
	SET @EmpDept = ''
	SET @GroupID = ''
	SET @OfficeEmail = ''
	SET @EmployedStatusCode = ''
	SET @IsSupervisor = ''
	SET @B08Code1 = ''
	SET @B08Code2 = ''
	SET @B08Code3 = ''
	SET @B08Code4 = ''
	SET @B08Code5 = ''
	SET @IsEnable = ''
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspUserInsert] 
		@UserID = @UserID
		,@UserName = @UserName
		,@UserCode = @UserCode
		,@AgentLoginID = @AgentLoginID
		,@AgentLoginCode = @AgentLoginCode
		,@EmployeeNo = @EmployeeNo
		,@NickName = @NickName
		,@EmpDept = @EmpDept
		,@GroupID = @GroupID
		,@OfficeEmail = @OfficeEmail
		,@EmployedStatusCode = @EmployedStatusCode
		,@IsSupervisor = @IsSupervisor
		,@B08Code1 = @B08Code1
		,@B08Code2 = @B08Code2
		,@B08Code3 = @B08Code3
		,@B08Code4 = @B08Code4
		,@B08Code5 = @B08Code5
		,@IsEnable = @IsEnable
		,@Creator = @Creator
		,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
	,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/11 14:11:34    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserInsert] (
	@UserID VARCHAR(20)
	,@UserName NVARCHAR(60)
	,@UserCode VARCHAR(50)
	,@AgentLoginID VARCHAR(20)
	,@AgentLoginCode VARCHAR(20)
	,@EmployeeNo VARCHAR(11)
	,@NickName NVARCHAR(50)
	,@EmpDept VARCHAR(20)
	,@GroupID VARCHAR(20)
	,@OfficeEmail VARCHAR(70)
	,@EmployedStatusCode VARCHAR(1)
	,@IsSupervisor BIT
	,@B08Code1 NVARCHAR(100)
	,@B08Code2 NVARCHAR(100)
	,@B08Code3 NVARCHAR(100)
	,@B08Code4 NVARCHAR(100)
	,@B08Code5 NVARCHAR(100)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbUser] (
			UserID
			,UserName
			,UserCode
			,AgentLoginID
			,AgentLoginCode
			,EmployeeNo
			,NickName
			,EmpDept
			,GroupID
			,OfficeEmail
			,EmployedStatusCode
			,IsSupervisor
			,B08Code1
			,B08Code2
			,B08Code3
			,B08Code4
			,B08Code5
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@UserID
			,@UserName
			,@UserCode
			,@AgentLoginID
			,@AgentLoginCode
			,@EmployeeNo
			,@NickName
			,@EmpDept
			,@GroupID
			,@OfficeEmail
			,@EmployedStatusCode
			,@IsSupervisor
			,@B08Code1
			,@B08Code2
			,@B08Code3
			,@B08Code4
			,@B08Code5
			,@IsEnable
			,GETDATE()
			,@Creator
			,GETDATE()
			,@Creator
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
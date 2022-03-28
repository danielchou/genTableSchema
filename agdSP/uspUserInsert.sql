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
	@UserId	VARCHAR(20) - 使用者ID
	@Password	NVARCHAR(100) - 密碼
	@UserName	NVARCHAR(50) - 使用者名稱
	@AgentId	NVARCHAR(20) - 經辦代碼
	@GroupId	NVARCHAR(20) - 部門代碼
	@ExtPhone	NVARCHAR(10) - 分機號碼
	@MobilePhone	NVARCHAR(20) - 手機號碼
	@Email	VARCHAR(50) - EMAIL
	@IsAdmin	BIT - 是否為主管
	@IsEnable	BIT - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@UserId VARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@GroupId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @UserId = 'A02344'
	SET @Password = '0000'
	SET @UserName = 'daniel'
	SET @AgentId = '0034'
	SET @GroupId = 'A01'
	SET @ExtPhone = '334'
	SET @MobilePhone = '0930744573'
	SET @Email = 'daniel@esun.bank.com'
	SET @IsAdmin = 1
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspUserInsert] 
	 @UserId = @UserId
	,@Password = @Password
	,@UserName = @UserName
	,@AgentId = @AgentId
	,@GroupId = @GroupId
	,@ExtPhone = @ExtPhone
	,@MobilePhone = @MobilePhone
	,@Email = @Email
	,@IsAdmin = @IsAdmin
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
** 2022-03-28 12:29:48    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserInsert] (
	@UserId VARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@GroupId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
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
			UserId
			,Password
			,UserName
			,AgentId
			,GroupId
			,ExtPhone
			,MobilePhone
			,Email
			,IsAdmin
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@UserId
			,@Password
			,@UserName
			,@AgentId
			,@GroupId
			,@ExtPhone
			,@MobilePhone
			,@Email
			,@IsAdmin
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
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
	@UserId NVARCHAR(20)	-	使用者帳號
	@Password NVARCHAR(100)	-	使用者密碼
	@UserName NVARCHAR(50)	-	使用者名稱
	@AgentId NVARCHAR(20)	-	AgentId
	@ExtPhone NVARCHAR(10)	-	分機號碼
	@MobilePhone NVARCHAR(20)	-	手機號碼
	@Email VARCHAR(50)	-	電子信箱
	@IsAdmin BIT	-	是否管理者
	@IsEnable BIT	-	是否啟用
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@UserId NVARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
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
	SET @ExtPhone = '334'
	SET @MobilePhone = '0930744573'
	SET @Email = 'daniel@esun.bank.com'
	SET @IsAdmin = '1'
	SET @IsEnable = '1'
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspUserInsert] 
	 @UserId = @UserId
	,@Password = @Password
	,@UserName = @UserName
	,@AgentId = @AgentId
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
** 2022-03-22 23:44:27    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspUserInsert] (
	@UserId NVARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
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
INSERT INTO [agdSet].[tbCode]
           (
			[UserId]
			,[Password]
			,[UserName]
			,[AgentId]
			,[DeptId]
			,[ExtPhone]
			,[MobilePhone]
			,[Email]
			,[IsAdmin]
			,[IsEnable]
			,[CreateDT]
			,[Creator]
			,[UpdateDT]
			,[Updator]
        )
		VALUES (
			@UserId
			,@Password
			,@UserName
			,@AgentId
			,@DeptId
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

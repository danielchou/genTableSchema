/****************************************************************
** Name: [agdSp].[uspUserUpdate]
** Desc: 使用者更新
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
    @SeqNo INT	-	使用者序號
	@UserId NVARCHAR(20)	-	使用者帳號
	@Password NVARCHAR(100)	-	使用者密碼
	@UserName NVARCHAR(50)	-	使用者名稱
	@AgentId NVARCHAR(20)	-	AgentId
	@ExtPhone NVARCHAR(10)	-	分機號碼
	@MobilePhone NVARCHAR(20)	-	手機號碼
	@Email VARCHAR(50)	-	電子信箱
	@IsAdmin BIT	-	是否管理者
	@IsEnable BIT	-	是否啟用
	@CreateDT DATETIME2	-	建立日期
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@UserId NVARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
	,@IsEnable BIT
	,@CreateDT DATETIME2
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 2
	SET @UserId = 'agent'
	SET @Password = ''
	SET @UserName = 'BBB'
	SET @AgentId = ''
	SET @ExtPhone = ''
	SET @MobilePhone = ''
	SET @Email = ''
	SET @IsAdmin = ''
	SET @IsEnable = ''
	SET @CreateDT = ''

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,@SeqNo = @SeqNo
	,@UserId = @UserId
	,@Password = @Password
	,@UserName = @UserName
	,@AgentId = @AgentId
	,@ExtPhone = @ExtPhone
	,@MobilePhone = @MobilePhone
	,@Email = @Email
	,@IsAdmin = @IsAdmin
	,@IsEnable = @IsEnable
	,@CreateDT = @CreateDT
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 00:40:38    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspUserUpdate] (
	@SeqNo INT
	,@UserId NVARCHAR(20)
	,@Password NVARCHAR(100)
	,@UserName NVARCHAR(50)
	,@AgentId NVARCHAR(20)
	,@ExtPhone NVARCHAR(10)
	,@MobilePhone NVARCHAR(20)
	,@Email VARCHAR(50)
	,@IsAdmin BIT
	,@IsEnable BIT
	,@CreateDT DATETIME2
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbUser
		SET UserId = @UserId
			,Password = @Password
			,UserName = @UserName
			,AgentId = @AgentId
			,DeptId = @DeptId
			,ExtPhone = @ExtPhone
			,MobilePhone = @MobilePhone
			,Email = @Email
			,IsAdmin = @IsAdmin
			,IsEnable = @IsEnable
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

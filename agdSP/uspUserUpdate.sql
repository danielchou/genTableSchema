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
    @SeqNo	INT - 流水號
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
	@Updator	VARCHAR(20) - 異動者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
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
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 'V'
	SET @UserId = 'A02344'
	SET @Password = '0000'
	SET @UserName = 'daniel'
	SET @AgentId = '0033'
	SET @GroupId = 'A02'
	SET @ExtPhone = '334'
	SET @MobilePhone = '0930744573'
	SET @Email = 'daniel@esun.bank.com'
	SET @IsAdmin = 1
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspUserUpdate]
    @SeqNo = @SeqNo
	,@UserId = @UserId
	,@Password = @Password
	,@UserName = @UserName
	,@AgentId = @AgentId
	,@GroupId = @GroupId
	,@ExtPhone = @ExtPhone
	,@MobilePhone = @MobilePhone
	,@Email = @Email
	,@IsAdmin = @IsAdmin
	,@IsEnable = @IsEnable
	,@Updator = @Updator
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-28 11:27:22    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserUpdate] (
	@SeqNo INT
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
	,@Updator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
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
			,GroupId = @GroupId
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
/****************************************************************
** Name: [agdSp].[uspUserGet]
** Desc: 使用者查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	使用者序號
**	UserId	NVARCHAR(20)	-	使用者帳號
**	Password	NVARCHAR(100)	-	使用者密碼
**	UserName	NVARCHAR(50)	-	使用者名稱
**	AgentId	NVARCHAR(20)	-	AgentId
**	ExtPhone	NVARCHAR(10)	-	分機號碼
**	MobilePhone	NVARCHAR(20)	-	手機號碼
**	Email	VARCHAR(50)	-	電子信箱
**	IsAdmin	BIT	-	是否管理者
**	IsEnable	BIT	-	是否啟用
**	CreateDT	DATETIME2	-	建立日期
**	Creator	NVARCHAR(20)	-	建立者
**	UpdateDT	DATETIME2	-	更新日期
**	Updator	NVARCHAR(20)	-	更新者
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	使用者序號
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
	DECLARE @return_value INT
		,@SeqNo INT
		,@ErrorMsg NVARCHAR(100)

	SET @SeqNo = 1

	EXEC @return_value = [agdSp].[uspUserGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-18 00:27:00    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspUserGet] (
	@SeqNo INT
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
			,f.Password
			,f.UserName
			,f.AgentId
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
		FROM agdSet.tbUser AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

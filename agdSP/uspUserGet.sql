/****************************************************************
** Name: [agdSp].[uspUserGet]
** Desc: 使用者查詢
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
**  CreateDT	 - 建立時間
**  UpdateDT	 - 異動時間
**	UpdatorName - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo	INT - 流水號
	@UserId	VARCHAR(20) - 使用者ID
	@UserName	NVARCHAR(50) - 使用者名稱
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

	SET @SeqNo = 2
	SET @UserId = 'agent'
	SET @UserName = 'BBB'

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
** 2022-03-28 12:29:47    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspUserGet] (
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
			,f.UserName
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
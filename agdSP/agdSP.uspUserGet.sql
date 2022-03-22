/****************************************************************
** Name: [agdSp].[uspUserGet]
** Desc: 使用者查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	使用者序號
**	UserId	NVARCHAR(20)	-	使用者帳號
**	UserName	NVARCHAR(50)	-	使用者名稱
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
** 2022-03-22 00:40:38    Daniel Chou	    first release
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

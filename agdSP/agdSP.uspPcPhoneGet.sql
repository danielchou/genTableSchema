/****************************************************************
** Name: [agdSp].[uspPcPhoneGet]
** Desc: 電腦電話查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	電腦電話序號
**	ExtCode	VARCHAR(20)	-	分機號碼
**	ComputerIP	VARCHAR(45)	-	電腦IP
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	電腦電話序號
	@ExtCode VARCHAR(20)	-	分機號碼
	@ComputerIP VARCHAR(45)	-	電腦IP
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

	EXEC @return_value = [agdSp].[uspPcPhoneGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 23:44:29    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspPcPhoneGet] (
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
			,f.ExtCode
			,f.ComputerIP
			,u.UserName AS UpdatorName
		FROM agdSet.tbPcPhone AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

/****************************************************************
** Name: [agdSp].[uspFuncGet]
** Desc: 系統功能查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	功能序號
**	FuncId	VARCHAR(20)	-	功能代碼
**	FuncName	NVARCHAR(50)	-	功能名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	功能序號
	@FuncId VARCHAR(20)	-	功能代碼
	@FuncName NVARCHAR(50)	-	功能名稱
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

	EXEC @return_value = [agdSp].[uspFuncGet] @SeqNo = @SeqNo
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
ALTER PROCEDURE [agdSp].[uspFuncGet] (
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
			,f.FuncId
			,f.FuncName
			,u.UserName AS UpdatorName
		FROM agdSet.tbFunc AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

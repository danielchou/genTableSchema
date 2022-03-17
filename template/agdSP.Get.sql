/****************************************************************
** Name: [agdSp].[usp{tb}Get]
** Desc: {tbDscr}查詢
**
** Return values: 0 成功
** Return Recordset: 
{pt_select}
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	{pt_input}
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

	EXEC @return_value = [agdSp].[usp{tb}Get] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** {pt_DateTime}    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[usp{tb}Get] (
	@SeqNo INT
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
			{pt_fCol}
			,u.UserName AS UpdatorName
		FROM agdSet.tb{tb} AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

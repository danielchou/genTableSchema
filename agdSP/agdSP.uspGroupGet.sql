/****************************************************************
** Name: [agdSp].[uspGroupGet]
** Desc: 群組單位查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	部門序號
**	GroupId	VARCHAR(20)	-	部門代碼
**	GroupName	NVARCHAR(50)	-	部門名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	部門序號
	@GroupId VARCHAR(20)	-	部門代碼
	@GroupName NVARCHAR(50)	-	部門名稱
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

	EXEC @return_value = [agdSp].[uspGroupGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 23:44:28    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspGroupGet] (
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
			,f.GroupId
			,f.GroupName
			,u.UserName AS UpdatorName
		FROM agdSet.tbGroup AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

/****************************************************************
** Name: [agdSp].[uspCodeGet]
** Desc: 系統代碼查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	代碼序號
**	CodeType	NVARCHAR(20)	-	代碼類型
**	CodeId	VARCHAR(20)	-	代碼
**	CodeName	NVARCHAR(20)	-	代碼名稱
**	IsEnable	BIT	-	是否啟用
**	CreateDT	DATETIME2	-	建立日期
**	Creator	NVARCHAR(20)	-	建立者
**	UpdateDT	DATETIME2	-	更新IP
**	Updator	NVARCHAR(20)	-	更新者
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	代碼序號
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

	EXEC @return_value = [agdSp].[uspCodeGet] @SeqNo = @SeqNo
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
ALTER PROCEDURE [agdSp].[uspCodeGet] (
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
			,f.CodeType
			,f.CodeId
			,f.CodeName
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbCode AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

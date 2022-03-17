/****************************************************************
** Name: [agdSp].[uspFuncGet]
** Desc: 系統功能查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	功能序號
**	FuncId	VARCHAR(20)	-	功能代碼
**	FuncName	NVARCHAR(50)	-	功能名稱
**	FuncPath	NVARCHAR(30)	-	功能路由
**	FuncIcon	NVARCHAR(30)	-	功能Icon
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
	@SeqNo INT	-	功能序號
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
** 2022-03-18 00:27:00    Daniel Chou	    first release
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
			,f.FuncPath
			,f.FuncIcon
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
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

/****************************************************************
** Name: [agdSp].[uspFuncGet]
** Desc: 系統功能查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  FuncId	 - 功能ID
**  FuncName	 - 功能名稱
**  FuncPath	 - 功能路由
**  FuncIcon	 - 功能圖示
**  IsEnable	 - 是否啟用?
**  Creator	 - 建立者
**  Updator	 - 異動者
**  CreateDt	 - 建立時間
**  UpdateDt	 - 異動時間
**	UpdatorName - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo	INT - 流水號
	@FuncId	VARCHAR(20) - 功能ID
	@FuncName	NVARCHAR(50) - 功能名稱
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
	SET @FuncId = 1
	SET @FuncName = '代碼設定'

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
** 2022-04-01 13:51:27    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncGet] (
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
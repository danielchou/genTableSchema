/****************************************************************
** Name: [agdSp].[uspCodeGet]
** Desc: 系統代碼查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  CodeType	 - 代碼分類
**  CodeId	 - 系統代碼檔代碼
**  CodeName	 - 系統代碼檔名稱
**  IsEnable	 - 是否啟用?
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
	@CodeType	NVARCHAR(20) - 代碼分類
	@CodeId	VARCHAR(20) - 系統代碼檔代碼
	@CodeName	NVARCHAR(50) - 系統代碼檔名稱
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
	SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'

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
** 2022-03-28 12:29:50    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeGet] (
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
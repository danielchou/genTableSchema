/****************************************************************
** Name: [agdSp].[uspGroupGet]
** Desc: 群組單位查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  GroupId	 - 群組ID
**  GroupName	 - 群組名稱
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
	@GroupId	VARCHAR(20) - 群組ID
	@GroupName	NVARCHAR(50) - 群組名稱
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
	SET @GroupId = 1
	SET @GroupName = '經辦'

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
** 2022-03-28 14:45:43    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupGet] (
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
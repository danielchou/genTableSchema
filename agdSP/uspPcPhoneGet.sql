/****************************************************************
** Name: [agdSp].[uspPcPhoneGet]
** Desc: 電腦電話查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - Seq No.
**  ComputerName	 - 電腦名稱
**  ComputerIp	 - IP 位址
**  ExtCode	 - 電話分機
**  Memo	 - 備註
**  IsEnable	 - 是否啟用?
**  Creator	 - 建立者
**  Updator	 - 更新者
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
	@SeqNo	INT - Seq No.
	@ExtCode	NVARCHAR(20) - 電話分機
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

	SET @SeqNo = '1'
	SET @ExtCode = '1111'

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
** 2022-04-01 13:51:30    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneGet] (
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
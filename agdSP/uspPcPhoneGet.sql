/****************************************************************
** Name: [agdSp].[uspPcPhoneGet]
** Desc: 電腦電話查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  ExtCode	 - 分機號碼
**  ComputerName	 - 電腦名稱
**  ComputerIP	 - 電腦IP
**  Memo	 - 備註
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
	@ExtCode	NVARCHAR(20) - 分機號碼
	@ComputerIP	NVARCHAR(50) - 電腦IP
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
	SET @ComputerIP = '1111'

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
** 2022-03-28 11:27:24    Daniel Chou	    first release
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
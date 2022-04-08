/****************************************************************
** Name: [agdSp].[uspPcPhoneGet]
** Desc: 電腦電話查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - Seq No.
** ExtCode          NVARCHAR(10) - 電話分機
** ComputerName     NVARCHAR(25) - 電腦名稱
** ComputerIp       NVARCHAR(23) - 電腦IP
** Memo             NVARCHAR(600) - 備註
** IsEnable         BIT          - 是否啟用?
** Creator          VARCHAR(20)  - 建立者
** Updator          VARCHAR(20)  - 更新者
** CreateDt         DATETIME2    - 建立時間
** UpdateDt         DATETIME2    - 異動時間
**	UpdatorName       NVARCHAR(20)   - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - Seq No.
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
** 2022/04/08 16:28:54    	Daniel Chou	    first release
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
			,f.ComputerName
			,f.ComputerIp
			,f.Memo
			,f.IsEnable
			,f.Creator
			,f.Updator
			,f.CreateDt
			,f.UpdateDt
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
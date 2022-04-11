/****************************************************************
** Name: [agdSp].[uspPcPhoneGet]
** Desc: 電腦電話配對查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** ComputerIP       VARCHAR(20)  - 電腦IP
** ComputerName     VARCHAR(50)  - 電腦名稱
** ExtCode          VARCHAR(20)  - 分機號碼
** Memo             NVARCHAR(200) - 備註
** CreateDT         DATETIME2(7) - 建立時間
** Creator          VARCHAR(20)  - 建立者
** UpdateDT         DATETIME2(7) - 更新時間
** Updator          VARCHAR(20)  - 更新者
**	UpdatorName       NVARCHAR(20)   - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - 流水號
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
** 2022/04/11 14:11:34    	Daniel Chou	    first release
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
			,f.ComputerIP
			,f.ComputerName
			,f.ExtCode
			,f.Memo
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
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
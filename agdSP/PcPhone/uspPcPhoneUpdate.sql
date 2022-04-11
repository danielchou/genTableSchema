/****************************************************************
** Name: [agdSp].[uspPcPhoneUpdate]
** Desc: 電腦電話配對更新
**
** Return values: 0 成功
** Return Recordset: 
**	NA
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @SeqNo           INT          - 流水號
	@ComputerIP      VARCHAR(20)  - 電腦IP
	@ComputerName    VARCHAR(50)  - 電腦名稱
	@ExtCode         VARCHAR(20)  - 分機號碼
	@Memo            NVARCHAR(200) - 備註
	@Updator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ComputerIP VARCHAR(20)
	,@ComputerName VARCHAR(50)
	,@ExtCode VARCHAR(20)
	,@Memo NVARCHAR(200)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ComputerIP = ''
	SET @ComputerName = ''
	SET @ExtCode = ''
	SET @Memo = ''
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneUpdate]
    @SeqNo = @SeqNo
		,@ComputerIP = @ComputerIP
		,@ComputerName = @ComputerName
		,@ExtCode = @ExtCode
		,@Memo = @Memo
	,@Updator = @Updator
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/11 14:11:34    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneUpdate] (
	@SeqNo INT
	,@ComputerIP VARCHAR(20)
	,@ComputerName VARCHAR(50)
	,@ExtCode VARCHAR(20)
	,@Memo NVARCHAR(200)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbPcPhone
		SET ComputerIP = @ComputerIP
			,ComputerName = @ComputerName
			,ExtCode = @ExtCode
			,Memo = @Memo
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
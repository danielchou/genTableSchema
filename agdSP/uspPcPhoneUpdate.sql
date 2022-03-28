/****************************************************************
** Name: [agdSp].[uspPcPhoneUpdate]
** Desc: 電腦電話更新
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
    @SeqNo	INT - 流水號
	@ExtCode	NVARCHAR(20) - 分機號碼
	@ComputerName	NVARCHAR(50) - 電腦名稱
	@ComputerIP	NVARCHAR(50) - 電腦IP
	@Memo	NVARCHAR(600) - 備註
	@IsEnable	BIT - 是否啟用?
	@Updator	VARCHAR(20) - 異動者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ExtCode = '1111'
	SET @ComputerName = 'CP0001'
	SET @ComputerIP = '1.1.1.1'
	SET @Memo = 'memo1'
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneUpdate]
    @SeqNo = @SeqNo
	,@ExtCode = @ExtCode
	,@ComputerName = @ComputerName
	,@ComputerIP = @ComputerIP
	,@Memo = @Memo
	,@IsEnable = @IsEnable
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
** 2022-03-28 11:27:24    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneUpdate] (
	@SeqNo INT
	,@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbPcPhone
		SET ExtCode = @ExtCode
			,ComputerName = @ComputerName
			,ComputerIP = @ComputerIP
			,Memo = @Memo
			,IsEnable = @IsEnable
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
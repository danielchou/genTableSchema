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
    @SeqNo INT	-	電腦電話序號
	@ExtCode VARCHAR(20)	-	分機號碼
	@ComputerName NVARCHAR(50)	-	電腦名稱
	@ComputerIP VARCHAR(45)	-	電腦IP
	@Memo NVARCHAR(600)	-	備註
	@IsEnable BIT	-	是否啟用
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP VARCHAR(45)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = '1'
	SET @ExtCode = '1111'
	SET @ComputerName = ''
	SET @ComputerIP = '1111'
	SET @Memo = ''
	SET @IsEnable = ''

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,@SeqNo = @SeqNo
	,@ExtCode = @ExtCode
	,@ComputerName = @ComputerName
	,@ComputerIP = @ComputerIP
	,@Memo = @Memo
	,@IsEnable = @IsEnable
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 23:44:29    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspPcPhoneUpdate] (
	@SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP VARCHAR(45)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
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

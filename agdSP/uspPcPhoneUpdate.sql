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
    @SeqNo	INT - Seq No.
	@ComputerName	NVARCHAR(20) - 電腦名稱
	@ExtCode	NVARCHAR(20) - 電話分機
	@Memo	NVARCHAR(600) - 備註
	@IsEnable	BIT - 是否啟用?
	@Updator	VARCHAR(20) - 更新者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ComputerName = 'CP0001'
	SET @ExtCode = '1111'
	SET @Memo = 'memo1'
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneUpdate]
    @SeqNo = @SeqNo
	,@ComputerName = @ComputerName
	,@ExtCode = @ExtCode
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
** 2022-04-01 13:51:31    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneUpdate] (
	@SeqNo INT
	,@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
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
		SET ComputerName = @ComputerName
			,ComputerIp = @ComputerIp
			,ExtCode = @ExtCode
			,Memo = @Memo
			,IsEnable = @IsEnable
			,CreateDt = @CreateDt
			,UpdateDt = @UpdateDt
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
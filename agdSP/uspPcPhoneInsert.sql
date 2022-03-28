/****************************************************************
** Name: [agdSp].[uspPcPhoneInsert]
** Desc: 電腦電話新增
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
	@ExtCode	NVARCHAR(20) - 分機號碼
	@ComputerName	NVARCHAR(50) - 電腦名稱
	@ComputerIP	NVARCHAR(50) - 電腦IP
	@Memo	NVARCHAR(600) - 備註
	@IsEnable	BIT - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @ExtCode = '1111'
	SET @ComputerName = 'CP0001'
	SET @ComputerIP = '1.1.1.1'
	SET @Memo = 'memo1'
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneInsert] 
	 @ExtCode = @ExtCode
	,@ComputerName = @ComputerName
	,@ComputerIP = @ComputerIP
	,@Memo = @Memo
	,@IsEnable = @IsEnable
	,@Creator = @Creator
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
CREATE PROCEDURE [agdSp].[uspPcPhoneInsert] (
	@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbPcPhone] (
			ExtCode
			,ComputerName
			,ComputerIP
			,Memo
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@ExtCode
			,@ComputerName
			,@ComputerIP
			,@Memo
			,@IsEnable
			,GETDATE()
			,@Creator
			,GETDATE()
			,@Creator
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
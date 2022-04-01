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
	@ComputerName	NVARCHAR(20) - 電腦名稱
	@ExtCode	NVARCHAR(20) - 電話分機
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
	,@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @ComputerName = 'CP0001'
	SET @ExtCode = '1111'
	SET @Memo = 'memo1'
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspPcPhoneInsert] 
	 @ComputerName = @ComputerName
	,@ExtCode = @ExtCode
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
** 2022-04-01 13:51:31    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneInsert] (
	@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
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
			ComputerName
			,ComputerIp
			,ExtCode
			,Memo
			,IsEnable
			,CreateDt
			,UpdateDt
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@ComputerName
			,@ComputerIp
			,@ExtCode
			,@Memo
			,@IsEnable
			,@CreateDt
			,@UpdateDt
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
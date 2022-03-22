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
	,@ExtCode VARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP VARCHAR(45)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @ExtCode = '1111'
	SET @ComputerName = 'CP001'
	SET @ComputerIP = '1.1.1.10'
	SET @Memo = 'tester2'
	SET @IsEnable = '1'
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
** 2022-03-22 23:44:29    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspPcPhoneInsert] (
	@ExtCode VARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP VARCHAR(45)
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
INSERT INTO [agdSet].[tbCode]
           (
			[ExtCode]
			,[ComputerName]
			,[ComputerIP]
			,[Memo]
			,[IsEnable]
			,[CreateDT]
			,[Creator]
			,[UpdateDT]
			,[Updator]
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

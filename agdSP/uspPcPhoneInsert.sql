/****************************************************************
** Name: [agdSp].[uspPcPhoneInsert]
** Desc: 電腦電話配對新增
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
	@ComputerIP      VARCHAR(20)  - 電腦IP
	@ComputerName    VARCHAR(50)  - 電腦名稱
	@ExtCode         VARCHAR(20)  - 分機號碼
	@Memo            NVARCHAR(200) - 備註
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@ComputerIP VARCHAR(20)
	,@ComputerName VARCHAR(50)
	,@ExtCode VARCHAR(20)
	,@Memo NVARCHAR(200)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @ComputerIP = '1234'
	SET @ComputerName = '1234'
	SET @ExtCode = '1234'
	SET @Memo = '1234'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspPcPhoneInsert] 
		@ComputerIP = @ComputerIP
		,@ComputerName = @ComputerName
		,@ExtCode = @ExtCode
		,@Memo = @Memo
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
** 2022/04/12 16:52:48    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneInsert] (
	@ComputerIP VARCHAR(20)
	,@ComputerName VARCHAR(50)
	,@ExtCode VARCHAR(20)
	,@Memo NVARCHAR(200)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbPcPhone] (
			ComputerIP
			,ComputerName
			,ExtCode
			,Memo
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@ComputerIP
			,@ComputerName
			,@ExtCode
			,@Memo
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
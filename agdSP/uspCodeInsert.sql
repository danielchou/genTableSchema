/****************************************************************
** Name: [agdSp].[uspCodeInsert]
** Desc: 系統代碼新增
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
	@CodeType	NVARCHAR(20) - 代碼分類
	@CodeId	VARCHAR(20) - 系統代碼檔代碼
	@CodeName	NVARCHAR(50) - 系統代碼檔名稱
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
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspCodeInsert] 
	 @CodeType = @CodeType
	,@CodeId = @CodeId
	,@CodeName = @CodeName
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
** 2022-04-01 13:51:30    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeInsert] (
	@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@IsEnable BIT    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbCode] (
			seqNo
			,CodeType
			,CodeId
			,CodeName
			,IsEnable
			,CreateDt
			,UpdateDt
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@seqNo
			,@CodeType
			,@CodeId
			,@CodeName
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
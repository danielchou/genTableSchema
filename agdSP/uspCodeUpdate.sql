/****************************************************************
** Name: [agdSp].[uspCodeUpdate]
** Desc: 系統代碼更新
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
	@Updator	VARCHAR(20) - 異動者
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
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspCodeUpdate]
    @CodeType = @CodeType
	,@CodeId = @CodeId
	,@CodeName = @CodeName
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
** 2022-04-01 13:51:30    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeUpdate] (
	@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbCode
		SET seqNo = @seqNo
			,CodeType = @CodeType
			,CodeId = @CodeId
			,CodeName = @CodeName
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
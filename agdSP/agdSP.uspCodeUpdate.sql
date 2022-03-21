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
    @SeqNo INT	-	代碼序號
	@CodeType NVARCHAR(20)	-	代碼類型
	@CodeId VARCHAR(20)	-	代碼
	@CodeName NVARCHAR(20)	-	代碼名稱
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
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(20)
	,@IsEnable BIT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = ''
	SET @CodeType = 'aux'
	SET @CodeId = 'B02'
	SET @CodeName = '休息'
	SET @IsEnable = 1

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,@SeqNo = @SeqNo
	,@CodeType = @CodeType
	,@CodeId = @CodeId
	,@CodeName = @CodeName
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
** 2022-03-22 00:40:40    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspCodeUpdate] (
	@SeqNo INT
	,@CodeType NVARCHAR(20)
	,@CodeId VARCHAR(20)
	,@CodeName NVARCHAR(20)
	,@IsEnable BIT
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbCode
		SET CodeType = @CodeType
			,CodeId = @CodeId
			,CodeName = @CodeName
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

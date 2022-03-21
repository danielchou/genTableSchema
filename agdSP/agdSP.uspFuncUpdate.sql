/****************************************************************
** Name: [agdSp].[uspFuncUpdate]
** Desc: 系統功能更新
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
    @SeqNo INT	-	功能序號
	@FuncId VARCHAR(20)	-	功能代碼
	@FuncName NVARCHAR(50)	-	功能名稱
	@FuncPath NVARCHAR(30)	-	功能路由
	@FuncIcon NVARCHAR(30)	-	功能Icon
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
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(30)
	,@FuncIcon NVARCHAR(30)
	,@IsEnable BIT
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = ''
	SET @FuncId = 1
	SET @FuncName = '代碼設定'
	SET @FuncPath = ''
	SET @FuncIcon = ''
	SET @IsEnable = ''

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,@SeqNo = @SeqNo
	,@FuncId = @FuncId
	,@FuncName = @FuncName
	,@FuncPath = @FuncPath
	,@FuncIcon = @FuncIcon
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
** 2022-03-22 00:40:39    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspFuncUpdate] (
	@SeqNo INT
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(30)
	,@FuncIcon NVARCHAR(30)
	,@IsEnable BIT
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbFunc
		SET FuncId = @FuncId
			,FuncName = @FuncName
			,FuncPath = @FuncPath
			,FuncIcon = @FuncIcon
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

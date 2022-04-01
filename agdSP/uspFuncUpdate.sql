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
    @SeqNo	INT - 流水號
	@FuncId	VARCHAR(20) - 功能ID
	@FuncName	NVARCHAR(50) - 功能名稱
	@FuncPath	NVARCHAR(20) - 功能路由
	@FuncIcon	NVARCHAR(20) - 功能圖示
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
    ,@SeqNo INT
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @FuncId = 'A0023'
	SET @FuncName = '職務權限設定'
	SET @FuncPath = '/admin/auth/index'
	SET @FuncIcon = 'fm-icon-home'
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspFuncUpdate]
    @SeqNo = @SeqNo
	,@FuncId = @FuncId
	,@FuncName = @FuncName
	,@FuncPath = @FuncPath
	,@FuncIcon = @FuncIcon
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
** 2022-04-01 13:51:27    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncUpdate] (
	@SeqNo INT
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
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
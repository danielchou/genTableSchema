/****************************************************************
** Name: [agdSp].[uspSysConfigUpdate]
** Desc: 系統參數更新
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
    @SeqNo           INT          - 流水號
	@SysConfigType   VARCHAR(20)  - 系統參數類別
	@SysConfigID     VARCHAR(20)  - 系統參數代碼
	@SysConfigName   NVARCHAR(50) - 系統參數名稱
	@Content         NVARCHAR(200) - 系統參數內容
	@IsVisible       BIT          - 是否顯示?
	@Updator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
	,@Content NVARCHAR(200)
	,@IsVisible BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @SysConfigType = '1234'
	SET @SysConfigID = '1234'
	SET @SysConfigName = '1234'
	SET @Content = '1234'
	SET @IsVisible = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspSysConfigUpdate]
    @SeqNo = @SeqNo
		,@SysConfigType = @SysConfigType
		,@SysConfigID = @SysConfigID
		,@SysConfigName = @SysConfigName
		,@Content = @Content
		,@IsVisible = @IsVisible
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
** 2022/04/12 16:52:51    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspSysConfigUpdate] (
	@SeqNo INT
	,@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
	,@Content NVARCHAR(200)
	,@IsVisible BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbSysConfig
		SET SysConfigType = @SysConfigType
			,SysConfigID = @SysConfigID
			,SysConfigName = @SysConfigName
			,Content = @Content
			,IsVisible = @IsVisible
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
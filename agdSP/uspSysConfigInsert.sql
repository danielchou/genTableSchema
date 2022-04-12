/****************************************************************
** Name: [agdSp].[uspSysConfigInsert]
** Desc: 系統參數新增
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
	@SysConfigType   VARCHAR(20)  - 系統參數類別
	@SysConfigID     VARCHAR(20)  - 系統參數代碼
	@SysConfigName   NVARCHAR(50) - 系統參數名稱
	@Content         NVARCHAR(200) - 系統參數內容
	@IsVisible       BIT          - 是否顯示?
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
	,@Content NVARCHAR(200)
	,@IsVisible BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @SysConfigType = '1234'
	SET @SysConfigID = '1234'
	SET @SysConfigName = '1234'
	SET @Content = '1234'
	SET @IsVisible = '1'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspSysConfigInsert] 
		@SysConfigType = @SysConfigType
		,@SysConfigID = @SysConfigID
		,@SysConfigName = @SysConfigName
		,@Content = @Content
		,@IsVisible = @IsVisible
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
** 2022/04/12 16:52:51    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspSysConfigInsert] (
	@SysConfigType VARCHAR(20)
	,@SysConfigID VARCHAR(20)
	,@SysConfigName NVARCHAR(50)
	,@Content NVARCHAR(200)
	,@IsVisible BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbSysConfig] (
			SysConfigType
			,SysConfigID
			,SysConfigName
			,Content
			,IsVisible
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@SysConfigType
			,@SysConfigID
			,@SysConfigName
			,@Content
			,@IsVisible
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
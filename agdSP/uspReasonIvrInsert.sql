/****************************************************************
** Name: [agdSp].[uspReasonIvrInsert]
** Desc: 聯繫原因Ivr配對新增
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
	@IvrID           VARCHAR(20)  - Ivr代碼
	@ReasonID        VARCHAR(20)  - 原因代碼
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @IvrID = '1234'
	SET @ReasonID = '1234'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspReasonIvrInsert] 
		@IvrID = @IvrID
		,@ReasonID = @ReasonID
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
CREATE PROCEDURE [agdSp].[uspReasonIvrInsert] (
	@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbReasonIvr] (
			IvrID
			,ReasonID
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@IvrID
			,@ReasonID
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
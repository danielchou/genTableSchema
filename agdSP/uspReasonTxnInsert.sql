/****************************************************************
** Name: [agdSp].[uspReasonTxnInsert]
** Desc: 聯繫原因Txn配對新增
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
	@TxnItem         VARCHAR(20)  - Txn交易類型
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
	,@TxnItem VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @TxnItem = '1234'
	SET @ReasonID = '1234'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspReasonTxnInsert] 
		@TxnItem = @TxnItem
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
CREATE PROCEDURE [agdSp].[uspReasonTxnInsert] (
	@TxnItem VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbReasonTxn] (
			TxnItem
			,ReasonID
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@TxnItem
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
/****************************************************************
** Name: [agdSp].[uspTxnUpdate]
** Desc: 交易執行更新
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
	@TxnType         VARCHAR(20)  - 交易執行類別
	@TxnID           VARCHAR(50)  - 交易執行代碼
	@TxnName         NVARCHAR(50) - 交易執行名稱
	@TxnScript       NVARCHAR(2000) - 交易執行話術
	@DisplayOrder    INT          - 顯示順序
	@IsEnable        BIT          - 是否啟用?
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
	,@TxnType VARCHAR(20)
	,@TxnID VARCHAR(50)
	,@TxnName NVARCHAR(50)
	,@TxnScript NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @TxnType = 'AGD'
	SET @TxnID = '1234'
	SET @TxnName = '1234'
	SET @TxnScript = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspTxnUpdate]
    @SeqNo = @SeqNo
		,@TxnType = @TxnType
		,@TxnID = @TxnID
		,@TxnName = @TxnName
		,@TxnScript = @TxnScript
		,@DisplayOrder = @DisplayOrder
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
** 2022/04/12 16:52:50    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspTxnUpdate] (
	@SeqNo INT
	,@TxnType VARCHAR(20)
	,@TxnID VARCHAR(50)
	,@TxnName NVARCHAR(50)
	,@TxnScript NVARCHAR(2000)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbTxn
		SET TxnType = @TxnType
			,TxnID = @TxnID
			,TxnName = @TxnName
			,TxnScript = @TxnScript
			,DisplayOrder = @DisplayOrder
			,IsEnable = @IsEnable
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
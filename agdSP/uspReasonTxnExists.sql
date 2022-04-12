/****************************************************************
** Name: [agdSp].[uspReasonTxnExists]
** Desc: 聯繫原因Txn配對查詢是否重複
**
** Return values: 0 成功
** Return Recordset: 
**	Total		:資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - 流水號
	@TxnItem         VARCHAR(20)  - Txn交易類型
	@ReasonID        VARCHAR(20)  - 原因代碼
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@TxnItem VARCHAR(20)
	,@ReasonID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @TxnItem = '1234'
	SET @ReasonID = '1234'

	EXEC @return_value = [agdSp].[uspReasonTxnExists] 
    	@SeqNo = @SeqNo
		,@TxnItem = @TxnItem
		,@ReasonID = @ReasonID
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:48    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspReasonTxnExists]
    @SeqNo INT
	,@TxnItem VARCHAR(20)
	,@ReasonID VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbReasonTxn
		WHERE SeqNo != @SeqNo
			AND ( 
                TxnItem = @TxnItem OR
				ReasonID = @ReasonID
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
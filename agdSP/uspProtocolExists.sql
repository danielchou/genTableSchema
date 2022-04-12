/****************************************************************
** Name: [agdSp].[uspProtocolExists]
** Desc: 通路碼查詢是否重複
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
	@Protocol        VARCHAR(20)  - 通路碼代碼
	@ProtocolName    NVARCHAR(50) - 通路碼名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @Protocol = '1234'
	SET @ProtocolName = '1234'

	EXEC @return_value = [agdSp].[uspProtocolExists] 
    	@SeqNo = @SeqNo
		,@Protocol = @Protocol
		,@ProtocolName = @ProtocolName
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
CREATE PROCEDURE [agdSp].[uspProtocolExists]
    @SeqNo INT
	,@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbProtocol
		WHERE SeqNo != @SeqNo
			AND ( 
                Protocol = @Protocol OR
				ProtocolName = @ProtocolName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
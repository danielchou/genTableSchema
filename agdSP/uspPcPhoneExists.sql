/****************************************************************
** Name: [agdSp].[uspPcPhoneExists]
** Desc: 電腦電話查詢是否重複
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
	@SeqNo	INT - Seq No.
	@ExtCode	NVARCHAR(20) - 電話分機
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ExtCode NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = '1'
	SET @ExtCode = '1111'

EXEC @return_value = [agdSp].[uspPcPhoneExists] 
    @SeqNo = @SeqNo
	,@ExtCode = @ExtCode
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-04-01 13:51:30    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneExists]
    @SeqNo INT
	,@ExtCode NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbPcPhone
		WHERE SeqNo != @SeqNo
			AND ( 
                ExtCode = @ExtCode
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
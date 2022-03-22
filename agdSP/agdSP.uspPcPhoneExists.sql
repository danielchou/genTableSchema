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
	@SeqNo INT	-	電腦電話序號
	@ExtCode VARCHAR(20)	-	分機號碼
	@ComputerIP VARCHAR(45)	-	電腦IP
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerIP VARCHAR(45)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = '1'
	SET @ExtCode = '1111'
	SET @ComputerIP = '1111'

EXEC @return_value = [agdSp].[uspPcPhoneExists] 
    @SeqNo = @SeqNo
	,@ExtCode = @ExtCode
	,@ComputerIP = @ComputerIP
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-22 23:44:29    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspPcPhoneExists]
    @SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerIP VARCHAR(45)
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
                ExtCode = @ExtCode OR
				ComputerIP = @ComputerIP
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

/****************************************************************
** Name: [agdSp].[uspPhoneBookExists]
** Desc: 電話簿查詢是否重複
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
	@PhoneBookID     VARCHAR(20)  - 電話簿代碼
	@PhoneBookName   NVARCHAR(50) - 電話簿名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @PhoneBookID = '1234'
	SET @PhoneBookName = '1234'

	EXEC @return_value = [agdSp].[uspPhoneBookExists] 
    	@SeqNo = @SeqNo
		,@PhoneBookID = @PhoneBookID
		,@PhoneBookName = @PhoneBookName
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
CREATE PROCEDURE [agdSp].[uspPhoneBookExists]
    @SeqNo INT
	,@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbPhoneBook
		WHERE SeqNo != @SeqNo
			AND ( 
                PhoneBookID = @PhoneBookID OR
				PhoneBookName = @PhoneBookName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
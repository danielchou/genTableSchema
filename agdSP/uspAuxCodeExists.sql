/****************************************************************
** Name: [agdSp].[uspAuxCodeExists]
** Desc: 休息碼查詢是否重複
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
	@AuxID           VARCHAR(20)  - 休息碼代碼
	@AuxName         NVARCHAR(50) - 休息碼名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @AuxID = '1234'
	SET @AuxName = '1234'

	EXEC @return_value = [agdSp].[uspAuxCodeExists] 
    	@SeqNo = @SeqNo
		,@AuxID = @AuxID
		,@AuxName = @AuxName
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:51    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspAuxCodeExists]
    @SeqNo INT
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbAuxCode
		WHERE SeqNo != @SeqNo
			AND ( 
                AuxID = @AuxID OR
				AuxName = @AuxName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
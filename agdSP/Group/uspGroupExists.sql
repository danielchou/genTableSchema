/****************************************************************
** Name: [agdSp].[uspGroupExists]
** Desc: 群組查詢是否重複
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
	@GroupID         VARCHAR(20)  - 群組代碼
	@GroupName       NVARCHAR(50) - 群組名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @GroupID = ''
	SET @GroupName = ''

	EXEC @return_value = [agdSp].[uspGroupExists] 
    	@GroupID = @GroupID
		,@GroupName = @GroupName
    	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/11 14:11:34    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupExists]
    @GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tbGroup
		WHERE SeqNo != @SeqNo
			AND ( 
                GroupID = @GroupID OR
				GroupName = @GroupName
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
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
	@SeqNo           INT          - 流水號
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
    ,@SeqNo INT
	,@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @GroupID = '1'
	SET @GroupName = 'aaa'

	EXEC @return_value = [agdSp].[uspGroupExists] 
    	@SeqNo = @SeqNo
		,@GroupID = @GroupID
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
** 2022/04/12 16:52:47    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupExists]
    @SeqNo INT
	,@GroupID VARCHAR(20)
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
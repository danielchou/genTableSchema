/****************************************************************
** Name: [agdSp].[uspGroupUpdate]
** Desc: 群組單位更新
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
    @SeqNo INT	-	部門序號
	@GroupId VARCHAR(20)	-	部門代碼
	@GroupName NVARCHAR(50)	-	部門名稱
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
    ,@SeqNo INT
	,@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = ''
	SET @GroupId = 1
	SET @GroupName = '經辦'

EXEC @return_value = [agdSp].[uspCodeUpdate]
    ,@SeqNo = @SeqNo
	,@GroupId = @GroupId
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
** 2022-03-22 00:40:39    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspGroupUpdate] (
	@SeqNo INT
	,@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbGroup
		SET GroupId = @GroupId
			,GroupName = @GroupName
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

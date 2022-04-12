/****************************************************************
** Name: [agdSp].[uspGroupUpdate]
** Desc: 群組更新
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
	@GroupID         VARCHAR(20)  - 群組代碼
	@GroupName       NVARCHAR(50) - 群組名稱
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
	,@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @GroupID = '1'
	SET @GroupName = 'aaa'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspGroupUpdate]
    @SeqNo = @SeqNo
		,@GroupID = @GroupID
		,@GroupName = @GroupName
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
** 2022/04/12 16:52:47    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupUpdate] (
	@SeqNo INT
	,@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbGroup
		SET GroupID = @GroupID
			,GroupName = @GroupName
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
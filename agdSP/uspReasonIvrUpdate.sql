/****************************************************************
** Name: [agdSp].[uspReasonIvrUpdate]
** Desc: 聯繫原因Ivr配對更新
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
	@IvrID           VARCHAR(20)  - Ivr代碼
	@ReasonID        VARCHAR(20)  - 原因代碼
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
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @IvrID = '1234'
	SET @ReasonID = '1234'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspReasonIvrUpdate]
    @SeqNo = @SeqNo
		,@IvrID = @IvrID
		,@ReasonID = @ReasonID
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
** 2022/04/12 16:52:48    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspReasonIvrUpdate] (
	@SeqNo INT
	,@IvrID VARCHAR(20)
	,@ReasonID VARCHAR(20)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbReasonIvr
		SET IvrID = @IvrID
			,ReasonID = @ReasonID
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
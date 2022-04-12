/****************************************************************
** Name: [agdSp].[uspCalendarUpdate]
** Desc: 班表資訊更新
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
	@UserID          VARCHAR(20)  - 使用者帳號
	@ScheduleDate    DATETIME2(7) - 排定日期
	@Content         NVARCHAR(50) - 班表內容
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
	,@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
	,@Content NVARCHAR(50)
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @UserID = '1234'
	SET @ScheduleDate = '2022-02-02 12:00:00'
	SET @Content = '1234'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspCalendarUpdate]
    @SeqNo = @SeqNo
		,@UserID = @UserID
		,@ScheduleDate = @ScheduleDate
		,@Content = @Content
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
** 2022/04/12 16:52:49    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCalendarUpdate] (
	@SeqNo INT
	,@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
	,@Content NVARCHAR(50)
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbCalendar
		SET UserID = @UserID
			,ScheduleDate = @ScheduleDate
			,Content = @Content
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
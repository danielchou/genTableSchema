/****************************************************************
** Name: [agdSp].[uspCalendarInsert]
** Desc: 班表資訊新增
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
	@UserID          VARCHAR(20)  - 使用者帳號
	@ScheduleDate    DATETIME2(7) - 排定日期
	@Content         NVARCHAR(50) - 班表內容
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
	,@Content NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @UserID = '1234'
	SET @ScheduleDate = '2022-02-02 12:00:00'
	SET @Content = '1234'
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspCalendarInsert] 
		@UserID = @UserID
		,@ScheduleDate = @ScheduleDate
		,@Content = @Content
		,@Creator = @Creator
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
CREATE PROCEDURE [agdSp].[uspCalendarInsert] (
	@UserID VARCHAR(20)
	,@ScheduleDate DATETIME2(7)
	,@Content NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbCalendar] (
			UserID
			,ScheduleDate
			,Content
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@UserID
			,@ScheduleDate
			,@Content
			,GETDATE()
			,@Creator
			,GETDATE()
			,@Creator
			);
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
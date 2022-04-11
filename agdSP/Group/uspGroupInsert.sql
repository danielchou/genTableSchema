/****************************************************************
** Name: [agdSp].[uspGroupInsert]
** Desc: 群組新增
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
	@GroupID         VARCHAR(20)  - 群組代碼
	@GroupName       NVARCHAR(50) - 群組名稱
	@Creator NVARCHAR(20) - 建立者
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
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @GroupID = ''
	SET @GroupName = ''
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspGroupInsert] 
		@GroupID = @GroupID
		,@GroupName = @GroupName
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
** 2022/04/11 14:11:34    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupInsert] (
	@GroupID VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbGroup] (
			GroupID
			,GroupName
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@GroupID
			,@GroupName
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
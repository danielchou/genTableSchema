/****************************************************************
** Name: [agdSp].[usp{tb}Insert]
** Desc: {tbDscr}新增
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
	{pt_input}
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,{pt_Declare}
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	{pt_insertSetVal}
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[usp{tb}Insert] 
	 {pt_Exec}
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
** {pt_DateTime}    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[usp{tb}Insert] (
	{pt_Declare}    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tb{tb}] (
			{pt_insertCols}
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			{pt_insertVals}
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
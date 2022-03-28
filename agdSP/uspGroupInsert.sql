/****************************************************************
** Name: [agdSp].[uspGroupInsert]
** Desc: 群組單位新增
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
	@GroupId	VARCHAR(20) - 群組ID
	@GroupName	NVARCHAR(50) - 群組名稱
	@IsEnable	BIT - 是否啟用?
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@IsEnable BIT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @GroupId = 'G01'
	SET @GroupName = 'admin'
	SET @IsEnable = 1
	SET @Creator = 'admin'

EXEC @return_value = [agdSp].[uspGroupInsert] 
	 @GroupId = @GroupId
	,@GroupName = @GroupName
	,@IsEnable = @IsEnable
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
** 2022-03-28 11:27:23    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupInsert] (
	@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@IsEnable BIT    
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbGroup] (
			GroupId
			,GroupName
			,IsEnable
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@GroupId
			,@GroupName
			,@IsEnable
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
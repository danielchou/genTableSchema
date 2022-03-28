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
    @SeqNo	INT - 流水號
	@GroupId	VARCHAR(20) - 群組ID
	@GroupName	NVARCHAR(50) - 群組名稱
	@IsEnable	BIT - 是否啟用?
	@Updator	VARCHAR(20) - 異動者
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
	,@IsEnable BIT
	,@Updator VARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @GroupId = 'G01'
	SET @GroupName = 'admin'
	SET @IsEnable = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspGroupUpdate]
    @SeqNo = @SeqNo
	,@GroupId = @GroupId
	,@GroupName = @GroupName
	,@IsEnable = @IsEnable
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
** 2022-03-28 11:27:23    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspGroupUpdate] (
	@SeqNo INT
	,@GroupId VARCHAR(20)
	,@GroupName NVARCHAR(50)
	,@IsEnable BIT
	,@Updator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbGroup
		SET GroupId = @GroupId
			,GroupName = @GroupName
			,IsEnable = @IsEnable
            ,UpdateDT = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
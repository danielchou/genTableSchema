/****************************************************************
** Name: [agdSp].[uspAuxCodeUpdate]
** Desc: 休息碼更新
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
	@AuxID           VARCHAR(20)  - 休息碼代碼
	@AuxName         NVARCHAR(50) - 休息碼名稱
	@IsLongTimeAux   BIT          - 是否長時間離開?
	@DisplayOrder    INT          - 顯示順序
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
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
	,@IsLongTimeAux BIT
	,@DisplayOrder INT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @AuxID = '1234'
	SET @AuxName = '1234'
	SET @IsLongTimeAux = '1'
	SET @DisplayOrder = 1
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspAuxCodeUpdate]
    @SeqNo = @SeqNo
		,@AuxID = @AuxID
		,@AuxName = @AuxName
		,@IsLongTimeAux = @IsLongTimeAux
		,@DisplayOrder = @DisplayOrder
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
** 2022/04/12 16:52:51    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspAuxCodeUpdate] (
	@SeqNo INT
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
	,@IsLongTimeAux BIT
	,@DisplayOrder INT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbAuxCode
		SET AuxID = @AuxID
			,AuxName = @AuxName
			,IsLongTimeAux = @IsLongTimeAux
			,DisplayOrder = @DisplayOrder
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
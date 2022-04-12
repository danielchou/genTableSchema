/****************************************************************
** Name: [agdSp].[uspAuxCodeInsert]
** Desc: 休息碼新增
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
	@AuxID           VARCHAR(20)  - 休息碼代碼
	@AuxName         NVARCHAR(50) - 休息碼名稱
	@IsLongTimeAux   BIT          - 是否長時間離開?
	@DisplayOrder    INT          - 顯示順序
	@Creator NVARCHAR(20) - 建立者
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
	,@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
	,@IsLongTimeAux BIT
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @AuxID = '1234'
	SET @AuxName = '1234'
	SET @IsLongTimeAux = '1'
	SET @DisplayOrder = 1
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspAuxCodeInsert] 
		@AuxID = @AuxID
		,@AuxName = @AuxName
		,@IsLongTimeAux = @IsLongTimeAux
		,@DisplayOrder = @DisplayOrder
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
** 2022/04/12 16:52:51    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspAuxCodeInsert] (
	@AuxID VARCHAR(20)
	,@AuxName NVARCHAR(50)
	,@IsLongTimeAux BIT
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbAuxCode] (
			AuxID
			,AuxName
			,IsLongTimeAux
			,DisplayOrder
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@AuxID
			,@AuxName
			,@IsLongTimeAux
			,@DisplayOrder
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
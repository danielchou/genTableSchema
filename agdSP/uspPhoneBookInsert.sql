/****************************************************************
** Name: [agdSp].[uspPhoneBookInsert]
** Desc: 電話簿新增
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
	@PhoneBookID     VARCHAR(20)  - 電話簿代碼
	@PhoneBookName   NVARCHAR(50) - 電話簿名稱
	@ParentPhoneBookID VARCHAR(20)  - 上層電話簿代碼
	@PhoneBookNumber VARCHAR(20)  - 電話號碼
	@Level           TINYINT      - 階層
	@Memo            NVARCHAR(200) - 備註
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
	,@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
	,@ParentPhoneBookID VARCHAR(20)
	,@PhoneBookNumber VARCHAR(20)
	,@Level TINYINT
	,@Memo NVARCHAR(200)
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100);

	SET @PhoneBookID = '1234'
	SET @PhoneBookName = '1234'
	SET @ParentPhoneBookID = '1234'
	SET @PhoneBookNumber = '1234'
	SET @Level = '1'
	SET @Memo = '1234'
	SET @DisplayOrder = 1
	SET @Creator = 'admin'

	EXEC @return_value = [agdSp].[uspPhoneBookInsert] 
		@PhoneBookID = @PhoneBookID
		,@PhoneBookName = @PhoneBookName
		,@ParentPhoneBookID = @ParentPhoneBookID
		,@PhoneBookNumber = @PhoneBookNumber
		,@Level = @Level
		,@Memo = @Memo
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
** 2022/04/12 16:52:48    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPhoneBookInsert] (
	@PhoneBookID VARCHAR(20)
	,@PhoneBookName NVARCHAR(50)
	,@ParentPhoneBookID VARCHAR(20)
	,@PhoneBookNumber VARCHAR(20)
	,@Level TINYINT
	,@Memo NVARCHAR(200)
	,@DisplayOrder INT
	,@Creator VARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
	INSERT INTO [agdSet].[tbPhoneBook] (
			PhoneBookID
			,PhoneBookName
			,ParentPhoneBookID
			,PhoneBookNumber
			,Level
			,Memo
			,DisplayOrder
			,CreateDT
			,Creator
			,UpdateDT
			,Updator
        )
		VALUES (
			@PhoneBookID
			,@PhoneBookName
			,@ParentPhoneBookID
			,@PhoneBookNumber
			,@Level
			,@Memo
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
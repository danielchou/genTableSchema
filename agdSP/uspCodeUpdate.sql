/****************************************************************
** Name: [agdSp].[uspCodeUpdate]
** Desc: 共用碼更新
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
	@CodeType        VARCHAR(20)  - 共用碼類別
	@CodeID          VARCHAR(20)  - 共用碼代碼
	@CodeName        NVARCHAR(50) - 共用碼名稱
	@Content         NVARCHAR(500) - 共用碼內容
	@Memo            NVARCHAR(200) - 備註
	@DisplayOrder    INT          - 顯示順序
	@IsEnable        BIT          - 是否啟用?
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
	,@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@Content NVARCHAR(500)
	,@Memo NVARCHAR(200)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @CodeType = '1234'
	SET @CodeID = '1234'
	SET @CodeName = '1234'
	SET @Content = '1234'
	SET @Memo = '1234'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspCodeUpdate]
    @SeqNo = @SeqNo
		,@CodeType = @CodeType
		,@CodeID = @CodeID
		,@CodeName = @CodeName
		,@Content = @Content
		,@Memo = @Memo
		,@DisplayOrder = @DisplayOrder
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
** 2022/04/12 16:52:48    Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspCodeUpdate] (
	@SeqNo INT
	,@CodeType VARCHAR(20)
	,@CodeID VARCHAR(20)
	,@CodeName NVARCHAR(50)
	,@Content NVARCHAR(500)
	,@Memo NVARCHAR(200)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbCode
		SET CodeType = @CodeType
			,CodeID = @CodeID
			,CodeName = @CodeName
			,Content = @Content
			,Memo = @Memo
			,DisplayOrder = @DisplayOrder
			,IsEnable = @IsEnable
            ,UpdateDt = GETDATE()
			,Updator = @Updator
		WHERE SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
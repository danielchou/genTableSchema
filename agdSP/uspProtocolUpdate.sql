/****************************************************************
** Name: [agdSp].[uspProtocolUpdate]
** Desc: 通路碼更新
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
	@Protocol        VARCHAR(20)  - 通路碼代碼
	@ProtocolName    NVARCHAR(50) - 通路碼名稱
	@Direction       VARCHAR(1)   - IN/OUT方向
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
	,@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
	,@Direction VARCHAR(1)
	,@DisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @Protocol = '1234'
	SET @ProtocolName = '1234'
	SET @Direction = '1'
	SET @DisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspProtocolUpdate]
    @SeqNo = @SeqNo
		,@Protocol = @Protocol
		,@ProtocolName = @ProtocolName
		,@Direction = @Direction
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
CREATE PROCEDURE [agdSp].[uspProtocolUpdate] (
	@SeqNo INT
	,@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
	,@Direction VARCHAR(1)
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
		UPDATE agdSet.tbProtocol
		SET Protocol = @Protocol
			,ProtocolName = @ProtocolName
			,Direction = @Direction
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
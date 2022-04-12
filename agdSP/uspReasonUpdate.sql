/****************************************************************
** Name: [agdSp].[uspReasonUpdate]
** Desc: 聯繫原因碼更新
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
	@ReasonID        VARCHAR(20)  - 聯繫原因碼代碼
	@ReasonName      NVARCHAR(50) - 聯繫原因碼名稱
	@ParentReasonID  VARCHAR(20)  - 上層聯繫原因碼代碼
	@Level           TINYINT      - 階層
	@BussinessUnit   VARCHAR(3)   - 事業處
	@BussinessB03Type VARCHAR(3)   - B03業務別
	@ReviewType      VARCHAR(3)   - 覆核類別
	@Memo            NVARCHAR(20) - 備註
	@WebUrl          NVARCHAR(200) - 網頁連結
	@KMUrl           NVARCHAR(200) - KM連結
	@DisplayOrder    INT          - 顯示順序
	@IsUsually       BIT          - 是否常用
	@UsuallyReasonName NVARCHAR(50) - 常用名稱
	@UsuallyDisplayOrder INT          - 常用顯示順序
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
	,@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
	,@ParentReasonID VARCHAR(20)
	,@Level TINYINT
	,@BussinessUnit VARCHAR(3)
	,@BussinessB03Type VARCHAR(3)
	,@ReviewType VARCHAR(3)
	,@Memo NVARCHAR(20)
	,@WebUrl NVARCHAR(200)
	,@KMUrl NVARCHAR(200)
	,@DisplayOrder INT
	,@IsUsually BIT
	,@UsuallyReasonName NVARCHAR(50)
	,@UsuallyDisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
    ,@ErrorMsg NVARCHAR(100)

    SET @SeqNo = 1
	SET @ReasonID = '1234'
	SET @ReasonName = '1234'
	SET @ParentReasonID = '1234'
	SET @Level = '1'
	SET @BussinessUnit = '1'
	SET @BussinessB03Type = '1'
	SET @ReviewType = '1'
	SET @Memo = '1234'
	SET @WebUrl = '1234'
	SET @KMUrl = '1234'
	SET @DisplayOrder = 1
	SET @IsUsually = '1'
	SET @UsuallyReasonName = '1234'
	SET @UsuallyDisplayOrder = 1
	SET @IsEnable = '1'
	SET @Updator = 'admin'

EXEC @return_value = [agdSp].[uspReasonUpdate]
    @SeqNo = @SeqNo
		,@ReasonID = @ReasonID
		,@ReasonName = @ReasonName
		,@ParentReasonID = @ParentReasonID
		,@Level = @Level
		,@BussinessUnit = @BussinessUnit
		,@BussinessB03Type = @BussinessB03Type
		,@ReviewType = @ReviewType
		,@Memo = @Memo
		,@WebUrl = @WebUrl
		,@KMUrl = @KMUrl
		,@DisplayOrder = @DisplayOrder
		,@IsUsually = @IsUsually
		,@UsuallyReasonName = @UsuallyReasonName
		,@UsuallyDisplayOrder = @UsuallyDisplayOrder
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
CREATE PROCEDURE [agdSp].[uspReasonUpdate] (
	@SeqNo INT
	,@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
	,@ParentReasonID VARCHAR(20)
	,@Level TINYINT
	,@BussinessUnit VARCHAR(3)
	,@BussinessB03Type VARCHAR(3)
	,@ReviewType VARCHAR(3)
	,@Memo NVARCHAR(20)
	,@WebUrl NVARCHAR(200)
	,@KMUrl NVARCHAR(200)
	,@DisplayOrder INT
	,@IsUsually BIT
	,@UsuallyReasonName NVARCHAR(50)
	,@UsuallyDisplayOrder INT
	,@IsEnable BIT
	,@Updator NVARCHAR(20)
	,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		UPDATE agdSet.tbReason
		SET ReasonID = @ReasonID
			,ReasonName = @ReasonName
			,ParentReasonID = @ParentReasonID
			,Level = @Level
			,BussinessUnit = @BussinessUnit
			,BussinessB03Type = @BussinessB03Type
			,ReviewType = @ReviewType
			,Memo = @Memo
			,WebUrl = @WebUrl
			,KMUrl = @KMUrl
			,DisplayOrder = @DisplayOrder
			,IsUsually = @IsUsually
			,UsuallyReasonName = @UsuallyReasonName
			,UsuallyDisplayOrder = @UsuallyDisplayOrder
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
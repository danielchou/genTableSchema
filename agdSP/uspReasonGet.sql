/****************************************************************
** Name: [agdSp].[uspReasonGet]
** Desc: 聯繫原因碼查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** ReasonID         VARCHAR(20)  - 聯繫原因碼代碼
** ReasonName       NVARCHAR(50) - 聯繫原因碼名稱
** ParentReasonID   VARCHAR(20)  - 上層聯繫原因碼代碼
** Level            TINYINT      - 階層
** BussinessUnit    VARCHAR(3)   - 事業處
** BussinessB03Type VARCHAR(3)   - B03業務別
** ReviewType       VARCHAR(3)   - 覆核類別
** Memo             NVARCHAR(20) - 備註
** WebUrl           NVARCHAR(200) - 網頁連結
** KMUrl            NVARCHAR(200) - KM連結
** DisplayOrder     INT          - 顯示順序
** IsUsually        BIT          - 是否常用
** UsuallyReasonName NVARCHAR(50) - 常用名稱
** UsuallyDisplayOrder INT          - 常用顯示順序
** IsEnable         BIT          - 是否啟用?
** CreateDT         DATETIME2(7) - 建立時間
** Creator          VARCHAR(20)  - 建立者
** UpdateDT         DATETIME2(7) - 更新時間
** Updator          VARCHAR(20)  - 更新者
**	UpdatorName       NVARCHAR(20)   - 更新者名稱
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo           INT          - 流水號
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
	DECLARE @return_value INT
		,@SeqNo INT
		,@ErrorMsg NVARCHAR(100)

	SET @SeqNo = 1

	EXEC @return_value = [agdSp].[uspReasonGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:48    	Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspReasonGet] (
	@SeqNo INT
	,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
	)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
			f.SeqNo
			,f.ReasonID
			,f.ReasonName
			,f.ParentReasonID
			,f.Level
			,f.BussinessUnit
			,f.BussinessB03Type
			,f.ReviewType
			,f.Memo
			,f.WebUrl
			,f.KMUrl
			,f.DisplayOrder
			,f.IsUsually
			,f.UsuallyReasonName
			,f.UsuallyDisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbReason AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
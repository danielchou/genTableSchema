/****************************************************************
** Name: agdSp.uspReasonQuery
** Desc: 聯繫原因碼進階查詢
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
** UpdatorName      NVARCHAR(20) - 更新者名稱
** Total            INT          - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @ReasonID        VARCHAR(20)  - 聯繫原因碼代碼
	@ReasonName      NVARCHAR(50) - 聯繫原因碼名稱
	@ParentReasonID  VARCHAR(20)  - 上層聯繫原因碼代碼
	@Level           TINYINT      - 階層
	@Page 			  INT 			- 頁數
	@RowsPerPage 	  INT 			- 每頁筆數
	@SortColumn 	  NVARCHAR(30) 	- 排序欄位
	@SortOrder 		  VARCHAR(10) 	- 排序順序
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
	,@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
	,@ParentReasonID VARCHAR(20)
	,@Level TINYINT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @ReasonID = '1234'
	SET @ReasonName = '1234'
	SET @ParentReasonID = '1234'
	SET @Level = '1'

EXEC @return_value = agdSp.uspReasonQuery
	@ReasonID = @ReasonID
		,@ReasonName = @ReasonName
		,@ParentReasonID = @ParentReasonID
		,@Level = @Level
	,@Page = @Page
	,@RowsPerPage = @RowsPerPage
	,@SortColumn = @SortColumn
	,@SortOrder = @SortOrder
	,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:48    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspReasonQuery] (
	@ReasonID VARCHAR(20)
	,@ReasonName NVARCHAR(50)
	,@ParentReasonID VARCHAR(20)
	,@Level TINYINT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
    ,@ErrorMsg NVARCHAR(100) = NULL OUTPUT
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
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbReason AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.ReasonID LIKE CASE WHEN @ReasonID = '' THEN f.ReasonID ELSE '%' + @ReasonID + '%' END
			AND f.ReasonName LIKE CASE WHEN @ReasonName = '' THEN f.ReasonName ELSE '%' + @ReasonName + '%' END
			AND f.ParentReasonID LIKE CASE WHEN @ParentReasonID = '' THEN f.ParentReasonID ELSE '%' + @ParentReasonID + '%' END
			AND f.Level LIKE CASE WHEN @Level = '' THEN f.Level ELSE '%' + @Level + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'ReasonID' AND @SortOrder = 'ASC' THEN f.ReasonID END ASC,
			CASE WHEN @SortColumn = 'ReasonID' AND @SortOrder = 'DESC' THEN f.ReasonID END DESC,
			CASE WHEN @SortColumn = 'ReasonName' AND @SortOrder = 'ASC' THEN f.ReasonName END ASC,
			CASE WHEN @SortColumn = 'ReasonName' AND @SortOrder = 'DESC' THEN f.ReasonName END DESC,
			CASE WHEN @SortColumn = 'ParentReasonID' AND @SortOrder = 'ASC' THEN f.ParentReasonID END ASC,
			CASE WHEN @SortColumn = 'ParentReasonID' AND @SortOrder = 'DESC' THEN f.ParentReasonID END DESC,
			CASE WHEN @SortColumn = 'Level' AND @SortOrder = 'ASC' THEN f.Level END ASC,
			CASE WHEN @SortColumn = 'Level' AND @SortOrder = 'DESC' THEN f.Level END DESC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'ASC' THEN f.DisplayOrder END ASC,
			CASE WHEN @SortColumn = 'DisplayOrder' AND @SortOrder = 'DESC' THEN f.DisplayOrder END DESC,
			CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'ASC' THEN f.IsEnable END ASC,
			CASE WHEN @SortColumn = 'IsEnable' AND @SortOrder = 'DESC' THEN f.IsEnable END DESC,
			CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'ASC' THEN f.UpdateDT END ASC,
			CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'DESC' THEN f.UpdateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
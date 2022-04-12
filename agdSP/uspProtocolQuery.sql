/****************************************************************
** Name: agdSp.uspProtocolQuery
** Desc: 通路碼進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** Protocol         VARCHAR(20)  - 通路碼代碼
** ProtocolName     NVARCHAR(50) - 通路碼名稱
** Direction        VARCHAR(1)   - IN/OUT方向
** DisplayOrder     INT          - 顯示順序
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
    @Protocol        VARCHAR(20)  - 通路碼代碼
	@ProtocolName    NVARCHAR(50) - 通路碼名稱
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
	,@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @Protocol = '1234'
	SET @ProtocolName = '1234'

EXEC @return_value = agdSp.uspProtocolQuery
	@Protocol = @Protocol
		,@ProtocolName = @ProtocolName
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
CREATE PROCEDURE [agdSp].[uspProtocolQuery] (
	@Protocol VARCHAR(20)
	,@ProtocolName NVARCHAR(50)
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
			,f.Protocol
			,f.ProtocolName
			,f.Direction
			,f.DisplayOrder
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbProtocol AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE f.Protocol LIKE CASE WHEN @Protocol = '' THEN f.Protocol ELSE '%' + @Protocol + '%' END
			AND f.ProtocolName LIKE CASE WHEN @ProtocolName = '' THEN f.ProtocolName ELSE '%' + @ProtocolName + '%' END
		------- Sort 排序條件 -------
		ORDER BY 
			CASE WHEN @SortColumn = 'Protocol' AND @SortOrder = 'ASC' THEN f.Protocol END ASC,
			CASE WHEN @SortColumn = 'Protocol' AND @SortOrder = 'DESC' THEN f.Protocol END DESC,
			CASE WHEN @SortColumn = 'ProtocolName' AND @SortOrder = 'ASC' THEN f.ProtocolName END ASC,
			CASE WHEN @SortColumn = 'ProtocolName' AND @SortOrder = 'DESC' THEN f.ProtocolName END DESC,
			CASE WHEN @SortColumn = 'Direction' AND @SortOrder = 'ASC' THEN f.Direction END ASC,
			CASE WHEN @SortColumn = 'Direction' AND @SortOrder = 'DESC' THEN f.Direction END DESC,
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
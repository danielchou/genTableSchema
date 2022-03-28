/****************************************************************
** Name: agdSp.uspFuncQuery
** Desc: 系統功能進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  FuncId	 - 功能ID
**  FuncName	 - 功能名稱
**  FuncPath	 - 功能路由
**  FuncIcon	 - 功能圖示
**  IsEnable	 - 是否啟用?
**  Creator	 - 建立者
**  Updator	 - 異動者
**  CreateDT	 - 建立時間
**  UpdateDT	 - 異動時間
**	UpdatorName - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @FuncId	VARCHAR(20) - 功能ID
	@FuncName	NVARCHAR(50) - 功能名稱
	@FuncPath	NVARCHAR(20) - 功能路由
	@FuncIcon	NVARCHAR(20) - 功能圖示
	@IsEnable	BIT - 是否啟用?
	@Page INT - 頁數
	@RowsPerPage INT - 每頁筆數
	@SortColumn NVARCHAR(30) - 排序欄位
	@SortOrder VARCHAR(10) - 排序順序
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example:
** -----------
DECLARE @return_value INT
	,@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @FuncId = 'A0023'
	SET @FuncName = '職務權限設定'
	SET @FuncPath = '/admin/auth/index'
	SET @FuncIcon = 'fm-icon-home'
	SET @IsEnable = 1

EXEC @return_value = agdSp.uspFuncQuery
	@FuncId = @FuncId
	,@FuncName = @FuncName
	,@FuncPath = @FuncPath
	,@FuncIcon = @FuncIcon
	,@IsEnable = @IsEnable
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
** 2022-03-28 14:45:43    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncQuery] (
	@FuncId VARCHAR(20)
	,@FuncName NVARCHAR(50)
	,@FuncPath NVARCHAR(20)
	,@FuncIcon NVARCHAR(20)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
)
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT
            f.FuncId
			,f.FuncName
			,f.FuncPath
			,f.FuncIcon
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbFunc AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.FuncId LIKE CASE WHEN @FuncId = '' THEN f.FuncId ELSE '%' + @FuncId + '%' END
				AND f.FuncName LIKE CASE WHEN @FuncName = '' THEN f.FuncName ELSE '%' + @FuncName + '%' END
				AND f.FuncPath LIKE CASE WHEN @FuncPath = '' THEN f.FuncPath ELSE '%' + @FuncPath + '%' END
				AND f.FuncIcon LIKE CASE WHEN @FuncIcon = '' THEN f.FuncIcon ELSE '%' + @FuncIcon + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'FuncId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'FuncId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'FuncName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'FuncName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'CreateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
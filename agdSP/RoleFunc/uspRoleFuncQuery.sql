/****************************************************************
** Name: agdSp.uspRoleFuncQuery
** Desc: 角色功能配對進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** RoleID           VARCHAR(20)  - 角色代碼
** FuncID           VARCHAR(20)  - 功能代碼
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
    @RoleID          VARCHAR(20)  - 角色代碼
	@FuncID          VARCHAR(20)  - 功能代碼
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
	,@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @RoleID = ''
	SET @FuncID = ''

EXEC @return_value = agdSp.uspRoleFuncQuery
	@RoleID = @RoleID
		,@FuncID = @FuncID
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
** 2022/04/11 14:11:34    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspRoleFuncQuery] (
	@RoleID VARCHAR(20)
	,@FuncID VARCHAR(20)
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
			,f.RoleID
			,f.FuncID
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbRoleFunc AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.RoleID LIKE CASE WHEN @RoleID = '' THEN f.RoleID ELSE '%' + @RoleID + '%' END
				AND f.FuncID LIKE CASE WHEN @FuncID = '' THEN f.FuncID ELSE '%' + @FuncID + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'RoleID' AND @SortOrder = 'ASC' THEN f.RoleID END ASC,
				CASE WHEN @SortColumn = 'RoleID' AND @SortOrder = 'DESC' THEN f.RoleID END DESC,
				CASE WHEN @SortColumn = 'FuncID' AND @SortOrder = 'ASC' THEN f.FuncID END ASC,
				CASE WHEN @SortColumn = 'FuncID' AND @SortOrder = 'DESC' THEN f.FuncID END DESC,
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
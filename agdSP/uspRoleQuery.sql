/****************************************************************
** Name: agdSp.uspRoleQuery
** Desc: 角色進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  RoleId	 - 角色代碼
**  RoleName	 - 角色名稱
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
    @RoleId	VARCHAR(20) - 角色代碼
	@RoleName	NVARCHAR(50) - 角色名稱
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
	,@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @RoleId = 'R01'
	SET @RoleName = 'admin'
	SET @IsEnable = 1

EXEC @return_value = agdSp.uspRoleQuery
	@RoleId = @RoleId
	,@RoleName = @RoleName
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
** 2022-03-28 12:29:49    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspRoleQuery] (
	@RoleId VARCHAR(20)
	,@RoleName NVARCHAR(50)
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
            f.RoleId
			,f.RoleName
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbRole AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.RoleId LIKE CASE WHEN @RoleId = '' THEN f.RoleId ELSE '%' + @RoleId + '%' END
				AND f.RoleName LIKE CASE WHEN @RoleName = '' THEN f.RoleName ELSE '%' + @RoleName + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'RoleId' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'RoleId' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'RoleName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'RoleName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
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
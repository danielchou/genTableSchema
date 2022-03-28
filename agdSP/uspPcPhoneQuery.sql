/****************************************************************
** Name: agdSp.uspPcPhoneQuery
** Desc: 電腦電話進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - 流水號
**  ExtCode	 - 分機號碼
**  ComputerName	 - 電腦名稱
**  ComputerIP	 - 電腦IP
**  Memo	 - 備註
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
    @ExtCode	NVARCHAR(20) - 分機號碼
	@ComputerName	NVARCHAR(50) - 電腦名稱
	@ComputerIP	NVARCHAR(50) - 電腦IP
	@Memo	NVARCHAR(600) - 備註
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
	,@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @ExtCode = '1111'
	SET @ComputerName = 'CP0001'
	SET @ComputerIP = '1.1.1.1'
	SET @Memo = 'memo1'
	SET @IsEnable = 'ALL'

EXEC @return_value = agdSp.uspPcPhoneQuery
	@ExtCode = @ExtCode
	,@ComputerName = @ComputerName
	,@ComputerIP = @ComputerIP
	,@Memo = @Memo
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
** 2022-03-28 11:27:24    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneQuery] (
	@ExtCode NVARCHAR(20)
	,@ComputerName NVARCHAR(50)
	,@ComputerIP NVARCHAR(50)
	,@Memo NVARCHAR(600)
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
            f.ExtCode
			,f.ComputerName
			,f.ComputerIP
			,f.Memo
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbPcPhone AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.ExtCode LIKE CASE WHEN @ExtCode = '' THEN f.ExtCode ELSE '%' + @ExtCode + '%' END
				AND f.ComputerName LIKE CASE WHEN @ComputerName = '' THEN f.ComputerName ELSE '%' + @ComputerName + '%' END
				AND f.ComputerIP LIKE CASE WHEN @ComputerIP = '' THEN f.ComputerIP ELSE '%' + @ComputerIP + '%' END
				AND f.Memo LIKE CASE WHEN @Memo = '' THEN f.Memo ELSE '%' + @Memo + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerIP' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerIP' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'Memo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'Memo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'UpdateDT' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
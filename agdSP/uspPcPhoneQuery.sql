/****************************************************************
** Name: agdSp.uspPcPhoneQuery
** Desc: 電腦電話進階查詢
**
** Return values: 0 成功
** Return Recordset: 
**  SeqNo	 - Seq No.
**  ComputerName	 - 電腦名稱
**  ComputerIp	 - IP 位址
**  ExtCode	 - 電話分機
**  Memo	 - 備註
**  IsEnable	 - 是否啟用?
**  Creator	 - 建立者
**  Updator	 - 更新者
**  CreateDt	 - 建立時間
**  UpdateDt	 - 異動時間
**	UpdatorName - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @ComputerName	NVARCHAR(20) - 電腦名稱
	@ExtCode	NVARCHAR(20) - 電話分機
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
	,@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
	,@Memo NVARCHAR(600)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @ComputerName = 'CP0001'
	SET @ExtCode = '1111'
	SET @Memo = 'memo1'
	SET @IsEnable = 1

EXEC @return_value = agdSp.uspPcPhoneQuery
	@ComputerName = @ComputerName
	,@ExtCode = @ExtCode
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
** 2022-04-01 13:51:30    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneQuery] (
	@ComputerName NVARCHAR(20)
	,@ExtCode NVARCHAR(20)
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
            f.ComputerName
			,f.ExtCode
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
		WHERE  f.ComputerName LIKE CASE WHEN @ComputerName = '' THEN f.ComputerName ELSE '%' + @ComputerName + '%' END
				AND f.ComputerIp LIKE CASE WHEN @ComputerIp = '' THEN f.ComputerIp ELSE '%' + @ComputerIp + '%' END
				AND f.ExtCode LIKE CASE WHEN @ExtCode = '' THEN f.ExtCode ELSE '%' + @ExtCode + '%' END
				AND f.Memo LIKE CASE WHEN @Memo = '' THEN f.Memo ELSE '%' + @Memo + '%' END
				AND f.CreateDt LIKE CASE WHEN @CreateDt = '' THEN f.CreateDt ELSE '%' + @CreateDt + '%' END
				AND f.UpdateDt LIKE CASE WHEN @UpdateDt = '' THEN f.UpdateDt ELSE '%' + @UpdateDt + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'SeqNo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerIp' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerIp' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'Memo' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'Memo' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'UpdateDt' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'UpdateDt' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
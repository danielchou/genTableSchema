/****************************************************************
** Name: agdSp.uspPcPhoneQuery
** Desc: 電腦電話進階查詢
**
** Return values: 0 成功
** Return Recordset: 
    @SeqNo INT	-	電腦電話序號
	@ExtCode VARCHAR(20)	-	分機號碼
	@ComputerIP VARCHAR(45)	-	電腦IP
**	UpdatorName NVARCHAR(20) - 更新者名稱
**  Total INT - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @SeqNo INT	-	電腦電話序號
	@ExtCode VARCHAR(20)	-	分機號碼
	@ComputerIP VARCHAR(45)	-	電腦IP
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
	@SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerIP VARCHAR(45)
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDT'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

SET @SeqNo = '1'
	SET @ExtCode = '1111'
	SET @ComputerIP = '1111'

EXEC @return_value = agdSp.uspPcPhoneQuery
	@SeqNo = @SeqNo
	,@ExtCode = @ExtCode
	,@ComputerIP = @ComputerIP
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
** 2022-03-22 23:44:29    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspPcPhoneQuery] (
	@SeqNo INT
	,@ExtCode VARCHAR(20)
	,@ComputerIP VARCHAR(45)
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
            f.SeqNo
			,f.ExtCode
			,f.ComputerIP
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
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerIP' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerIP' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

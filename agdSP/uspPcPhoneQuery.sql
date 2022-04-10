/****************************************************************
** Name: agdSp.uspPcPhoneQuery
** Desc: 電腦電話進階查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - Seq No.
** ExtCode          NVARCHAR(10) - 電話分機
** ComputerName     NVARCHAR(25) - 電腦名稱
** ComputerIp       NVARCHAR(23) - 電腦IP
** Memo             NVARCHAR(600) - 備註
** IsEnable         BIT          - 是否啟用?
** Creator          VARCHAR(20)  - 建立者
** Updator          VARCHAR(20)  - 更新者
** CreateDt         DATETIME2    - 建立時間
** UpdateDt         DATETIME2    - 異動時間
** UpdatorName      NVARCHAR(20) - 更新者名稱
** Total            INT          - 資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
    @ExtCode         NVARCHAR(10) - 電話分機
	@ComputerName    NVARCHAR(25) - 電腦名稱
	@IsEnable        BIT          - 是否啟用?
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
	,@ExtCode NVARCHAR(10)
	,@ComputerName NVARCHAR(25)
	,@IsEnable BIT
	,@Page INT = 1
	,@RowsPerPage INT = 20
	,@SortColumn NVARCHAR(30) = 'CreateDt'
	,@SortOrder VARCHAR(10) = 'ASC'
	,@ErrorMsg NVARCHAR(100)

	SET @ExtCode = '1111'
	SET @ComputerName = 'CP0001'
	SET @IsEnable = '1'

EXEC @return_value = agdSp.uspPcPhoneQuery
	@ExtCode = @ExtCode
		,@ComputerName = @ComputerName
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
** 2022/04/08 16:28:54    Daniel Chou     first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspPcPhoneQuery] (
	@ExtCode NVARCHAR(10)
	,@ComputerName NVARCHAR(25)
	,@IsEnable BIT
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
			,f.ExtCode
			,f.ComputerName
			,f.ComputerIp
			,f.Memo
			,f.IsEnable
			,f.Creator
			,f.Updator
			,f.CreateDt
			,f.UpdateDt
			,u.UserName AS UpdatorName
			,COUNT(f.SeqNo) OVER () AS Total
		FROM agdSet.tbPcPhone AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		------- WHERE 查詢條件 -------
		WHERE  f.ExtCode LIKE CASE WHEN @ExtCode = '' THEN f.ExtCode ELSE '%' + @ExtCode + '%' END
				AND f.ComputerName LIKE CASE WHEN @ComputerName = '' THEN f.ComputerName ELSE '%' + @ComputerName + '%' END
				AND f.IsEnable = CASE WHEN @IsEnable = 'ALL' THEN f.IsEnable ELSE CASE WHEN @IsEnable = '1' THEN 1 ELSE 0 END END
		------- Sort 排序條件 -------
		ORDER BY 
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ExtCode' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'ASC' THEN f.CreateDT END ASC,
				CASE WHEN @SortColumn = 'ComputerName' AND @SortOrder = 'DESC' THEN f.CreateDT END DESC
		------- Page 分頁條件 -------
		OFFSET @RowsPerPage * (@page - 1) ROWS

		FETCH NEXT @RowsPerPage ROWS ONLY
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
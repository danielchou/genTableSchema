/****************************************************************
** Name: [agdSp].[uspPhoneBookGet]
** Desc: 電話簿查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** PhoneBookID      VARCHAR(20)  - 電話簿代碼
** PhoneBookName    NVARCHAR(50) - 電話簿名稱
** ParentPhoneBookID VARCHAR(20)  - 上層電話簿代碼
** PhoneBookNumber  VARCHAR(20)  - 電話號碼
** Level            TINYINT      - 階層
** Memo             NVARCHAR(200) - 備註
** DisplayOrder     INT          - 顯示順序
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

	EXEC @return_value = [agdSp].[uspPhoneBookGet] @SeqNo = @SeqNo
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
CREATE PROCEDURE [agdSp].[uspPhoneBookGet] (
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
			,f.PhoneBookID
			,f.PhoneBookName
			,f.ParentPhoneBookID
			,f.PhoneBookNumber
			,f.Level
			,f.Memo
			,f.DisplayOrder
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbPhoneBook AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
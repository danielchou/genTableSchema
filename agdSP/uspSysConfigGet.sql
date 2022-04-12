/****************************************************************
** Name: [agdSp].[uspSysConfigGet]
** Desc: 系統參數查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** SysConfigType    VARCHAR(20)  - 系統參數類別
** SysConfigID      VARCHAR(20)  - 系統參數代碼
** SysConfigName    NVARCHAR(50) - 系統參數名稱
** Content          NVARCHAR(200) - 系統參數內容
** IsVisible        BIT          - 是否顯示?
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

	EXEC @return_value = [agdSp].[uspSysConfigGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/12 16:52:51    	Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspSysConfigGet] (
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
			,f.SysConfigType
			,f.SysConfigID
			,f.SysConfigName
			,f.Content
			,f.IsVisible
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbSysConfig AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
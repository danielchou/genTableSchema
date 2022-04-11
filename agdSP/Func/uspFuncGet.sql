/****************************************************************
** Name: [agdSp].[uspFuncGet]
** Desc: 功能查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** FuncID           VARCHAR(20)  - 功能代碼
** FuncName         NVARCHAR(50) - 功能名稱
** ParentFuncID     VARCHAR(20)  - 上層功能代碼
** Level            TINYINT      - 階層
** SystemType       VARCHAR(20)  - 系統類別
** RouteName        VARCHAR(50)  - 路由名稱
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

	

	EXEC @return_value = [agdSp].[uspFuncGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022/04/11 14:11:34    	Daniel Chou	    first release
*****************************************************************/
CREATE PROCEDURE [agdSp].[uspFuncGet] (
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
			,f.FuncID
			,f.FuncName
			,f.ParentFuncID
			,f.Level
			,f.SystemType
			,f.RouteName
			,f.DisplayOrder
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbFunc AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
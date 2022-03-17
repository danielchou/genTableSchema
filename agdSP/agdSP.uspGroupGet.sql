/****************************************************************
** Name: [agdSp].[uspGroupGet]
** Desc: 群組單位查詢
**
** Return values: 0 成功
** Return Recordset: 
**	SeqNo	INT	-	部門序號
**	GroupId	VARCHAR(20)	-	部門代碼
**	GroupName	NVARCHAR(50)	-	部門名稱
**	IsEnable	BIT	-	是否啟用
**	CreateDT	DATETIME2	-	建立日期
**	Creator	NVARCHAR(20)	-	建立者
**	CreateIP	VARCHAR(45)	-	建立IP
**	UpdateDT	DATETIME2	-	更新日期
**	Updator	NVARCHAR(20)	-	更新者
**	UpdateIP	VARCHAR(45)	-	更新IP
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	@SeqNo INT	-	部門序號
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

	EXEC @return_value = [agdSp].[uspGroupGet] @SeqNo = @SeqNo
		,@ErrorMsg = @ErrorMsg OUTPUT

	SELECT @return_value AS 'Return Value'
		,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** 2022-03-18 00:27:00    Daniel Chou	    first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[uspGroupGet] (
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
			,f.GroupId
			,f.GroupName
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.CreateIP
			,f.UpdateDT
			,f.Updator
			,f.UpdateIP
			,u.UserName AS UpdatorName
		FROM agdSet.tbGroup AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

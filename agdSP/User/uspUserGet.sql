/****************************************************************
** Name: [agdSp].[uspUserGet]
** Desc: 使用者查詢
**
** Return values: 0 成功
** Return Recordset: 
** SeqNo            INT          - 流水號
** UserID           VARCHAR(20)  - 使用者帳號
** UserName         NVARCHAR(60) - 使用者名稱
** UserCode         VARCHAR(50)  - 使用者代碼
** AgentLoginID     VARCHAR(20)  - CTI登入帳號
** AgentLoginCode   VARCHAR(20)  - CTI登入代碼
** EmployeeNo       VARCHAR(11)  - 員工編號
** NickName         NVARCHAR(50) - 使用者暱稱
** EmpDept          VARCHAR(20)  - 所屬單位
** GroupID          VARCHAR(20)  - 群組代碼
** OfficeEmail      VARCHAR(70)  - 公司Email
** EmployedStatusCode VARCHAR(1)   - 在職狀態代碼
** IsSupervisor     BIT          - 是否為主管?
** B08Code1         NVARCHAR(100) - B08_code1
** B08Code2         NVARCHAR(100) - B08_code2
** B08Code3         NVARCHAR(100) - B08_code3
** B08Code4         NVARCHAR(100) - B08_code4
** B08Code5         NVARCHAR(100) - B08_code5
** IsEnable         BIT          - 是否啟用?
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

	EXEC @return_value = [agdSp].[uspUserGet] @SeqNo = @SeqNo
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
CREATE PROCEDURE [agdSp].[uspUserGet] (
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
			,f.UserID
			,f.UserName
			,f.UserCode
			,f.AgentLoginID
			,f.AgentLoginCode
			,f.EmployeeNo
			,f.NickName
			,f.EmpDept
			,f.GroupID
			,f.OfficeEmail
			,f.EmployedStatusCode
			,f.IsSupervisor
			,f.B08Code1
			,f.B08Code2
			,f.B08Code3
			,f.B08Code4
			,f.B08Code5
			,f.IsEnable
			,f.CreateDT
			,f.Creator
			,f.UpdateDT
			,f.Updator
			,u.UserName AS UpdatorName
		FROM agdSet.tbUser AS f
		JOIN agdSet.tbUser AS u ON u.UserId = f.Updator
		WHERE f.SeqNo = @SeqNo;
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF
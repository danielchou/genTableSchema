/****************************************************************
** Name: [agdSp].[usp{tb}Exists]
** Desc: {tbDscr}查詢是否重複
**
** Return values: 0 成功
** Return Recordset: 
**	Total		:資料總筆數
**
** Called by: 
**	AGD WebApi
**
** Parameters:
**	Input
** -----------
	{pt_input}
**
**   Output
** -----------
	@ErrorMsg NVARCHAR(100) - 錯誤回傳訊息
** 
** Example: 
** -----------
DECLARE @return_value INT
    ,{pt_Declare}
    ,@ErrorMsg NVARCHAR(100)

    {pt_SetValue}

EXEC @return_value = [agdSp].[usp{tb}Exists] 
    {pt_Exec}
    ,@ErrorMsg = @ErrorMsg OUTPUT

SELECT @return_value AS 'Return Value'
    ,@ErrorMsg AS N'@ErrorMsg'
**
*****************************************************************
** Change History
*****************************************************************
** Date:            Author:         Description:
** ---------- ------- ------------------------------------
** {pt_DateTime}    Daniel Chou     first release
*****************************************************************/
ALTER PROCEDURE [agdSp].[usp{tb}Exists]
    {pt_Declare}
    ,@ErrorMsg NVARCHAR(100) =NULL OUTPUT
AS
SET NOCOUNT ON
SET @ErrorMsg = N''

BEGIN
	BEGIN TRY
		SELECT COUNT(SeqNo) AS Total
		FROM agdSet.tb{tb}
		WHERE SeqNo != @SeqNo
			AND ( 
                {pt_fColOr}
            );
	END TRY

	BEGIN CATCH
		SELECT @ErrorMsg = LEFT(ERROR_MESSAGE(), 100)
	END CATCH
END

SET NOCOUNT OFF

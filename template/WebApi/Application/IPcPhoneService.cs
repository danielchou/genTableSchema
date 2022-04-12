using ESUN.AGD.WebApi.Application.$pt_TableName.Contract;

namespace ESUN.AGD.WebApi.Application.$pt_TableName
{
    public interface I$pt_TableName$service
    {
        /// <summary>
        /// 依序號取得$TbDscr設定
        /// </summary>
        /// <param>
$pt_ColDscr_ParasPK
        /// </param>
        /// <returns>
$pt_ColDscr_GetReturnAll
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<$pt_TableName$response>> Get$pt_TableName(int seqNo);

        /// <summary>
        /// 搜尋$TbDscr設定 
        /// </summary>
        /// <param name="request">
$pt_ColDscr_QueryParas_Query
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
$pt_ColDscr_GetReturnAll
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<$pt_TableName$response>>> Query$pt_TableName($pt_TableName$queryRequest request);

        /// <summary>
        /// 新增$TbDscr設定 
        /// </summary>
        /// <param name="request">
$pt_ColDscr_InsertParas
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Insert$pt_TableName($pt_TableName$insertRequest request);

        /// <summary>
        /// 更新$TbDscr設定
        /// </summary>
        /// <param name="request">
$pt_colDscr_UpdateParas
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Update$pt_TableName($pt_TableName$updateRequest request);

        /// <summary>
        /// 刪除$TbDscr設定
        /// </summary>
        /// <param>
$pt_ColDscr_ParasPK
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Delete$pt_TableName($pt_InputPK);

        /// <summary>
        /// 檢查$TbDscr是否存在
        /// </summary>
        /// <param>
$pt_colDscr_ExistsParas
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists($pt_InputIsExist);

    }
}

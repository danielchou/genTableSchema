using ESUN.AGD.WebApi.Application.Code.Contract;

namespace ESUN.AGD.WebApi.Application.Code
{
    public interface ICodeService
    {
        /// <summary>
        /// 依序號取得共用碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<CodeResponse>> GetCode(int seqNo);

        /// <summary>
        /// 搜尋共用碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<CodeResponse>>> QueryCode(CodeQueryRequest request);

        /// <summary>
        /// 新增共用碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertCode(CodeInsertRequest request);

        /// <summary>
        /// 更新共用碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
		/// content          - string     - 共用碼內容
		/// memo             - string     - 備註
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateCode(CodeUpdateRequest request);

        /// <summary>
        /// 刪除共用碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteCode(int seqNo);

        /// <summary>
        /// 檢查共用碼是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 共用碼類別
		/// codeID           - string     - 共用碼代碼
		/// codeName         - string     - 共用碼名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string codeType,string codeID,string codeName);

    }
}

using ESUN.AGD.WebApi.Application.Code.Contract;

namespace ESUN.AGD.WebApi.Application.Code
{
    public interface ICodeService
    {
        /// <summary>
        /// 依序號取得系統代碼設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<CodeResponse>> GetCode(int seqNo);

        /// <summary>
        /// 搜尋系統代碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// creator          - string     - 建立者
		/// updator          - string     - 異動者
		/// createDt         - DateTime   - 建立時間
		/// updateDt         - DateTime   - 異動時間
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<CodeResponse>>> QueryCode(CodeQueryRequest request);

        /// <summary>
        /// 新增系統代碼設定 
        /// </summary>
        /// <param name="request">
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertCode(CodeInsertRequest request);

        /// <summary>
        /// 更新系統代碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名稱
		/// isEnable         - bool       - 是否啟用?
		/// updator          - string     - 異動者
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> UpdateCode(CodeUpdateRequest request);

        /// <summary>
        /// 刪除系統代碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> DeleteCode(int seqNo);

        /// <summary>
        /// 檢查系統代碼是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// codeType         - string     - 代碼分類
		/// codeId           - string     - 系統代碼檔代碼
		/// codeName         - string     - 系統代碼檔名
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string codeType,string codeId,string codeName);

    }
}

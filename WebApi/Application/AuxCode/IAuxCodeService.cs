using ESUN.AGD.WebApi.Application.AuxCode.Contract;

namespace ESUN.AGD.WebApi.Application.AuxCode
{
    public interface IAuxCodeService
    {
        /// <summary>
        /// 依序號取得休息碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<AuxCodeResponse>> GetAuxCode(int seqNo);

        /// <summary>
        /// 搜尋休息碼設定 
        /// </summary>
        /// <param name="request">
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<AuxCodeResponse>>> QueryAuxCode(AuxCodeQueryRequest request);

        /// <summary>
        /// 新增休息碼設定 
        /// </summary>
        /// <param name="request">
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertAuxCode(AuxCodeInsertRequest request);

        /// <summary>
        /// 更新休息碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
		/// isLongTimeAux    - bool       - 是否長時間離開?
		/// displayOrder     - int        - 顯示順序
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateAuxCode(AuxCodeUpdateRequest request);

        /// <summary>
        /// 刪除休息碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteAuxCode(int seqNo);

        /// <summary>
        /// 檢查休息碼是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// auxID            - string     - 休息碼代碼
		/// auxName          - string     - 休息碼名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string auxID,string auxName);

    }
}

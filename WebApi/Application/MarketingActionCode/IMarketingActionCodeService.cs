using ESUN.AGD.WebApi.Application.MarketingActionCode.Contract;

namespace ESUN.AGD.WebApi.Application.MarketingActionCode
{
    public interface IMarketingActionCodeService
    {
        /// <summary>
        /// 依序號取得行銷方案結果設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<MarketingActionCodeResponse>> GetMarketingActionCode(int seqNo);

        /// <summary>
        /// 搜尋行銷方案結果設定 
        /// </summary>
        /// <param name="request">
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<MarketingActionCodeResponse>>> QueryMarketingActionCode(MarketingActionCodeQueryRequest request);

        /// <summary>
        /// 新增行銷方案結果設定 
        /// </summary>
        /// <param name="request">
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertMarketingActionCode(MarketingActionCodeInsertRequest request);

        /// <summary>
        /// 更新行銷方案結果設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// actionCodeType   - string     - 客群方案類別
		/// marketingID      - string     - 行銷方案代碼
		/// actionCode       - string     - 行銷結果代碼
		/// content          - string     - 行銷結果說明
		/// isAccept         - bool       - 是否接受?
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateMarketingActionCode(MarketingActionCodeUpdateRequest request);

        /// <summary>
        /// 刪除行銷方案結果設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteMarketingActionCode(int seqNo);

        /// <summary>
        /// 檢查行銷方案結果是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// marketingID      - string     - 行銷方案代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string marketingID);

    }
}

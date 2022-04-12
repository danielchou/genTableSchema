using ESUN.AGD.WebApi.Application.Marketing.Contract;

namespace ESUN.AGD.WebApi.Application.Marketing
{
    public interface IMarketingService
    {
        /// <summary>
        /// 依序號取得行銷方案設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// marketingID      - string     - 行銷方案代碼
		/// marketingType    - string     - 行銷方案類別
		/// marketingName    - string     - 行銷方案名稱
		/// content          - string     - 行銷方案內容
		/// marketingScript  - string     - 行銷方案話術
		/// marketingBegintDT - DateTime   - 開始日期
		/// marketingEndDT   - DateTime   - 結束日期
		/// offerCode        - string     - 專案識別碼
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<MarketingResponse>> GetMarketing(int seqNo);

        /// <summary>
        /// 搜尋行銷方案設定 
        /// </summary>
        /// <param name="request">
		/// marketingID      - string     - 行銷方案代碼
		/// marketingType    - string     - 行銷方案類別
		/// marketingName    - string     - 行銷方案名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// marketingID      - string     - 行銷方案代碼
		/// marketingType    - string     - 行銷方案類別
		/// marketingName    - string     - 行銷方案名稱
		/// content          - string     - 行銷方案內容
		/// marketingScript  - string     - 行銷方案話術
		/// marketingBegintDT - DateTime   - 開始日期
		/// marketingEndDT   - DateTime   - 結束日期
		/// offerCode        - string     - 專案識別碼
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<MarketingResponse>>> QueryMarketing(MarketingQueryRequest request);

        /// <summary>
        /// 新增行銷方案設定 
        /// </summary>
        /// <param name="request">
		/// marketingID      - string     - 行銷方案代碼
		/// marketingType    - string     - 行銷方案類別
		/// marketingName    - string     - 行銷方案名稱
		/// content          - string     - 行銷方案內容
		/// marketingScript  - string     - 行銷方案話術
		/// marketingBegintDT - DateTime   - 開始日期
		/// marketingEndDT   - DateTime   - 結束日期
		/// offerCode        - string     - 專案識別碼
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertMarketing(MarketingInsertRequest request);

        /// <summary>
        /// 更新行銷方案設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// marketingID      - string     - 行銷方案代碼
		/// marketingType    - string     - 行銷方案類別
		/// marketingName    - string     - 行銷方案名稱
		/// content          - string     - 行銷方案內容
		/// marketingScript  - string     - 行銷方案話術
		/// marketingBegintDT - DateTime   - 開始日期
		/// marketingEndDT   - DateTime   - 結束日期
		/// offerCode        - string     - 專案識別碼
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateMarketing(MarketingUpdateRequest request);

        /// <summary>
        /// 刪除行銷方案設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteMarketing(int seqNo);

        /// <summary>
        /// 檢查行銷方案是否存在
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

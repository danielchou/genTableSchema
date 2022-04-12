using ESUN.AGD.WebApi.Application.SysConfig.Contract;

namespace ESUN.AGD.WebApi.Application.SysConfig
{
    public interface ISysConfigService
    {
        /// <summary>
        /// 依序號取得系統參數設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<SysConfigResponse>> GetSysConfig(int seqNo);

        /// <summary>
        /// 搜尋系統參數設定 
        /// </summary>
        /// <param name="request">
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<SysConfigResponse>>> QuerySysConfig(SysConfigQueryRequest request);

        /// <summary>
        /// 新增系統參數設定 
        /// </summary>
        /// <param name="request">
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertSysConfig(SysConfigInsertRequest request);

        /// <summary>
        /// 更新系統參數設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// sysConfigType    - string     - 系統參數類別
		/// sysConfigID      - string     - 系統參數代碼
		/// sysConfigName    - string     - 系統參數名稱
		/// content          - string     - 系統參數內容
		/// isVisible        - bool       - 是否顯示?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateSysConfig(SysConfigUpdateRequest request);

        /// <summary>
        /// 刪除系統參數設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteSysConfig(int seqNo);

        /// <summary>
        /// 檢查系統參數是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// sysConfigID      - string     - 系統參數代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string sysConfigID);

    }
}

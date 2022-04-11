using ESUN.AGD.WebApi.Application.PcPhone.Contract;

namespace ESUN.AGD.WebApi.Application.PcPhone
{
    public interface IPcPhoneService
    {
        /// <summary>
        /// 依序號取得電腦電話配對設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<PcPhoneResponse>> GetPcPhone(int seqNo);

        /// <summary>
        /// 搜尋電腦電話配對設定 
        /// </summary>
        /// <param name="request">
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<PcPhoneResponse>>> QueryPcPhone(PcPhoneQueryRequest request);

        /// <summary>
        /// 新增電腦電話配對設定 
        /// </summary>
        /// <param name="request">
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertPcPhone(PcPhoneInsertRequest request);

        /// <summary>
        /// 更新電腦電話配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// computerIP       - string     - 電腦IP
		/// computerName     - string     - 電腦名稱
		/// extCode          - string     - 分機號碼
		/// memo             - string     - 備註
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdatePcPhone(PcPhoneUpdateRequest request);

        /// <summary>
        /// 刪除電腦電話配對設定
        /// </summary>
        /// <param>
		/// computerIP       - string     - 電腦IP
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeletePcPhone(string computerIP);

        /// <summary>
        /// 檢查電腦電話配對是否存在
        /// </summary>
        /// <param>
		/// computerIP       - string     - 電腦IP
		/// extCode          - string     - 分機號碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(string computerIP,string extCode);

    }
}

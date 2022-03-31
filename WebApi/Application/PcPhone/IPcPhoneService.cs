using ESUN.AGD.WebApi.Application.PcPhone.Contract;

namespace ESUN.AGD.WebApi.Application.PcPhone
{
    public interface IPcPhoneService
    {
        /// <summary>
        /// 依序號取得電腦電話設定
        /// </summary>
        /// <param>
        /// seqNo - 電腦電話序號
        /// </param>
        /// <returns>
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Creator          - string     - 建立者
		/// Updator          - string     - 更新者
		/// CreateDt         - DateTime   - 建立時間
		/// UpdateDt         - DateTime   - 異動時間
        /// updatorName - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<PcPhoneResponse>> GetPcPhone(int seqNo);

        /// <summary>
        /// 搜尋電腦電話設定 
        /// </summary>
        /// <param name="request">
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - bool       - 排序順序
        /// </param>
        /// <returns>
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Creator          - string     - 建立者
		/// Updator          - string     - 更新者
		/// CreateDt         - DateTime   - 建立時間
		/// UpdateDt         - DateTime   - 異動時間
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<PcPhoneResponse>>> QueryPcPhone(PcPhoneQueryRequest request);

        /// <summary>
        /// 新增電腦電話設定 
        /// </summary>
        /// <param name="request">
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertPcPhone(PcPhoneInsertRequest request);

        /// <summary>
        /// 更新電腦電話設定
        /// </summary>
        /// <param name="request">
		/// SeqNo            - int        - Seq No.
		/// ComputerName     - string     - 電腦名稱
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分機
		/// Memo             - string     - 備註
		/// IsEnable         - bool       - 是否啟用?
		/// Updator          - string     - 更新者
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> UpdatePcPhone(PcPhoneUpdateRequest request);

        /// <summary>
        /// 刪除電腦電話設定
        /// </summary>
        /// <param>
		/// SeqNo            - int        - Seq No.
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> DeletePcPhone(int seqNo);

        /// <summary>
        /// 檢查電腦電話是否存在
        /// </summary>
        /// <param>
		/// SeqNo            - int        - Seq No.
		/// ComputerIp       - string     - IP 位址
		/// ExtCode          - string     - 電話分
        /// </param>
        /// <returns></returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string computerIp,string extCode);

    }
}

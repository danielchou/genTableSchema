using ESUN.AGD.WebApi.Application.Protocol.Contract;

namespace ESUN.AGD.WebApi.Application.Protocol
{
    public interface IProtocolService
    {
        /// <summary>
        /// 依序號取得通路碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<ProtocolResponse>> GetProtocol(int seqNo);

        /// <summary>
        /// 搜尋通路碼設定 
        /// </summary>
        /// <param name="request">
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<ProtocolResponse>>> QueryProtocol(ProtocolQueryRequest request);

        /// <summary>
        /// 新增通路碼設定 
        /// </summary>
        /// <param name="request">
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertProtocol(ProtocolInsertRequest request);

        /// <summary>
        /// 更新通路碼設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
		/// direction        - string     - IN/OUT方向
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateProtocol(ProtocolUpdateRequest request);

        /// <summary>
        /// 刪除通路碼設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteProtocol(int seqNo);

        /// <summary>
        /// 檢查通路碼是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// protocol         - string     - 通路碼代碼
		/// protocolName     - string     - 通路碼名稱
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string protocol,string protocolName);

    }
}

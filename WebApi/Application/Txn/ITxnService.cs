using ESUN.AGD.WebApi.Application.Txn.Contract;

namespace ESUN.AGD.WebApi.Application.Txn
{
    public interface ITxnService
    {
        /// <summary>
        /// 依序號取得交易執行設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<TxnResponse>> GetTxn(int seqNo);

        /// <summary>
        /// 搜尋交易執行設定 
        /// </summary>
        /// <param name="request">
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<TxnResponse>>> QueryTxn(TxnQueryRequest request);

        /// <summary>
        /// 新增交易執行設定 
        /// </summary>
        /// <param name="request">
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertTxn(TxnInsertRequest request);

        /// <summary>
        /// 更新交易執行設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// txnType          - string     - 交易執行類別
		/// txnID            - string     - 交易執行代碼
		/// txnName          - string     - 交易執行名稱
		/// txnScript        - string     - 交易執行話術
		/// displayOrder     - int        - 顯示順序
		/// isEnable         - bool       - 是否啟用?
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateTxn(TxnUpdateRequest request);

        /// <summary>
        /// 刪除交易執行設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteTxn(int seqNo);

        /// <summary>
        /// 檢查交易執行是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// txnID            - string     - 交易執行代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string txnID);

    }
}

using ESUN.AGD.WebApi.Application.ReasonTxn.Contract;

namespace ESUN.AGD.WebApi.Application.ReasonTxn
{
    public interface IReasonTxnService
    {
        /// <summary>
        /// 依序號取得聯繫原因Txn配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns>
        ValueTask<BasicResponse<ReasonTxnResponse>> GetReasonTxn(int seqNo);

        /// <summary>
        /// 搜尋聯繫原因Txn配對設定 
        /// </summary>
        /// <param name="request">
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// page             - int        - 分頁
        /// rowsPerPage      - int        - 每頁筆數
        /// sortColumn       - string     - 排序欄位
        /// sortOrder        - string     - 排序順序
        /// </param>
        /// <returns>
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
		/// createDT         - DateTime   - 建立時間
		/// creator          - string     - 建立者
		/// updateDT         - DateTime   - 更新時間
		/// updator          - string     - 更新者
        /// updatorName      - string     - 更新者名稱
        /// </returns> 
        ValueTask<BasicResponse<List<ReasonTxnResponse>>> QueryReasonTxn(ReasonTxnQueryRequest request);

        /// <summary>
        /// 新增聯繫原因Txn配對設定 
        /// </summary>
        /// <param name="request">
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> InsertReasonTxn(ReasonTxnInsertRequest request);

        /// <summary>
        /// 更新聯繫原因Txn配對設定
        /// </summary>
        /// <param name="request">
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> UpdateReasonTxn(ReasonTxnUpdateRequest request);

        /// <summary>
        /// 刪除聯繫原因Txn配對設定
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> DeleteReasonTxn(int seqNo);

        /// <summary>
        /// 檢查聯繫原因Txn配對是否存在
        /// </summary>
        /// <param>
		/// seqNo            - int        - 流水號
		/// txnItem          - string     - Txn交易類型
		/// reasonID         - string     - 原因代碼
        /// </param>
        /// <returns>
        /// </returns>
        ValueTask<BasicResponse<bool>> Exists(int seqNo,string txnItem,string? reasonID);

    }
}

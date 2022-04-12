

namespace ESUN.AGD.WebApi.Application.ReasonTxn.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class ReasonTxnQueryRequest : CommonQuery
    {
		/// <summary>
        /// Txn交易類型
        /// </summary>
        public string? txnItem { get; set; }
		/// <summary>
        /// 原因代碼
        /// </summary>
        public string? reasonID { get; set; }

    }
}

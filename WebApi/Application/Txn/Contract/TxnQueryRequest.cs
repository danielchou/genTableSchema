

namespace ESUN.AGD.WebApi.Application.Txn.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class TxnQueryRequest : CommonQuery
    {
		/// <summary>
        /// 交易執行類別
        /// </summary>
        public string? txnType { get; set; }
		/// <summary>
        /// 交易執行代碼
        /// </summary>
        public string? txnID { get; set; }
		/// <summary>
        /// 交易執行名稱
        /// </summary>
        public string? txnName { get; set; }

    }
}

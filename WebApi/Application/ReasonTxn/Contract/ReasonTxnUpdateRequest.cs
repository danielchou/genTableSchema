namespace ESUN.AGD.WebApi.Application.ReasonTxn.Contract
{
    /// <summary>
    /// 聯繫原因Txn配對更新請求
    /// </summary>
    public class ReasonTxnUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// Txn交易類型
        /// </summary>
        public string txnItem { get; set; }
		/// <summary>
        /// 原因代碼
        /// </summary>
        public string? reasonID { get; set; }
    }
}

namespace ESUN.AGD.WebApi.Application.ReasonTxn.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class ReasonTxnInsertRequest : CommonInsertRequest
    {
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
namespace ESUN.AGD.WebApi.Application.ReasonTxn.Contract
{
    /// <summary>
    /// 聯繫原因Txn配對回傳結果
    /// </summary>
    public class  ReasonTxnResponse : CommonResponse
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

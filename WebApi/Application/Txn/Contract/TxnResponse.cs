namespace ESUN.AGD.WebApi.Application.Txn.Contract
{
    /// <summary>
    /// 交易執行回傳結果
    /// </summary>
    public class  TxnResponse : CommonResponse
    {
        /// <summary>
        /// 交易執行類別
        /// </summary>
        public string txnType { get; set; }
        /// <summary>
        /// 交易執行代碼
        /// </summary>
        public string txnID { get; set; }
        /// <summary>
        /// 交易執行名稱
        /// </summary>
        public string txnName { get; set; }
        /// <summary>
        /// 交易執行話術
        /// </summary>
        public string? txnScript { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}

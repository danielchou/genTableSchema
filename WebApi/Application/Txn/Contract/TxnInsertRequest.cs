namespace ESUN.AGD.WebApi.Application.Txn.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class TxnInsertRequest : CommonInsertRequest
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
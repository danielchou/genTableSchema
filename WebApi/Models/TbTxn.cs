namespace ESUN.AGD.WebApi.Models
{
    public partial class TbTxn : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 交易執行類別
        /// </summary>
        public string TxnType { get; set; }  = null!;
        /// <summary>
        /// 交易執行代碼
        /// </summary>
        public string TxnID { get; set; }  = null!;
        /// <summary>
        /// 交易執行名稱
        /// </summary>
        public string TxnName { get; set; }  = null!;
        /// <summary>
        /// 交易執行話術
        /// </summary>
        public string? TxnScript { get; set; } 
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int DisplayOrder { get; set; } 
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool IsEnable { get; set; } 
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDT { get; set; } 
        /// <summary>
        /// 建立者
        /// </summary>
        public string Creator { get; set; }  = null!;
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDT { get; set; } 
        /// <summary>
        /// 更新者
        /// </summary>
        public string Updator { get; set; }  = null!;
    }
}


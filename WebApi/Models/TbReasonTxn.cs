namespace ESUN.AGD.WebApi.Models
{
    public partial class TbReasonTxn : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// Txn交易類型
        /// </summary>
        public string TxnItem { get; set; }  = null!;
        /// <summary>
        /// 原因代碼
        /// </summary>
        public string? ReasonID { get; set; } 
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


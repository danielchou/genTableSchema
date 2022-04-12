namespace ESUN.AGD.WebApi.Models
{
    public partial class TbMarketingActionCode : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 客群方案類別
        /// </summary>
        public string ActionCodeType { get; set; }  = null!;
        /// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string MarketingID { get; set; }  = null!;
        /// <summary>
        /// 行銷結果代碼
        /// </summary>
        public string ActionCode { get; set; }  = null!;
        /// <summary>
        /// 行銷結果說明
        /// </summary>
        public string Content { get; set; }  = null!;
        /// <summary>
        /// 是否接受?
        /// </summary>
        public bool IsAccept { get; set; } 
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


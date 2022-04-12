namespace ESUN.AGD.WebApi.Models
{
    public partial class TbMarketing : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string MarketingID { get; set; }  = null!;
        /// <summary>
        /// 行銷方案類別
        /// </summary>
        public string MarketingType { get; set; }  = null!;
        /// <summary>
        /// 行銷方案名稱
        /// </summary>
        public string MarketingName { get; set; }  = null!;
        /// <summary>
        /// 行銷方案內容
        /// </summary>
        public string Content { get; set; }  = null!;
        /// <summary>
        /// 行銷方案話術
        /// </summary>
        public string? MarketingScript { get; set; } 
        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime MarketingBegintDT { get; set; } 
        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime MarketingEndDT { get; set; } 
        /// <summary>
        /// 專案識別碼
        /// </summary>
        public string? OfferCode { get; set; } 
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


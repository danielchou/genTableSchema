namespace ESUN.AGD.WebApi.Application.Marketing.Contract
{
    /// <summary>
    /// 行銷方案回傳結果
    /// </summary>
    public class  MarketingResponse : CommonResponse
    {
        /// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string marketingID { get; set; }
        /// <summary>
        /// 行銷方案類別
        /// </summary>
        public string marketingType { get; set; }
        /// <summary>
        /// 行銷方案名稱
        /// </summary>
        public string marketingName { get; set; }
        /// <summary>
        /// 行銷方案內容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 行銷方案話術
        /// </summary>
        public string? marketingScript { get; set; }
        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime marketingBegintDT { get; set; }
        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime marketingEndDT { get; set; }
        /// <summary>
        /// 專案識別碼
        /// </summary>
        public string? offerCode { get; set; }
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

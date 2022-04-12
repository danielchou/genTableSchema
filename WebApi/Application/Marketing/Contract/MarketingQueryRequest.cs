

namespace ESUN.AGD.WebApi.Application.Marketing.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class MarketingQueryRequest : CommonQuery
    {
		/// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string? marketingID { get; set; }
		/// <summary>
        /// 行銷方案類別
        /// </summary>
        public string? marketingType { get; set; }
		/// <summary>
        /// 行銷方案名稱
        /// </summary>
        public string? marketingName { get; set; }

    }
}



namespace ESUN.AGD.WebApi.Application.MarketingActionCode.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class MarketingActionCodeQueryRequest : CommonQuery
    {
		/// <summary>
        /// 客群方案類別
        /// </summary>
        public string? actionCodeType { get; set; }
		/// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string? marketingID { get; set; }
		/// <summary>
        /// 行銷結果代碼
        /// </summary>
        public string? actionCode { get; set; }

    }
}

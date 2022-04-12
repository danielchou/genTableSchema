

namespace ESUN.AGD.WebApi.Application.MessageSheet.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class MessageSheetQueryRequest : CommonQuery
    {
		/// <summary>
        /// 訊息傳送頁籤類別
        /// </summary>
        public string? messageSheetType { get; set; }
		/// <summary>
        /// 訊息傳送頁籤代碼
        /// </summary>
        public string? messageSheetID { get; set; }
		/// <summary>
        /// 訊息傳送頁籤名稱
        /// </summary>
        public string? messageSheetName { get; set; }

    }
}



namespace ESUN.AGD.WebApi.Application.Message.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class MessageQueryRequest : CommonQuery
    {
		/// <summary>
        /// 訊息傳送頁籤代碼
        /// </summary>
        public string? messageSheetID { get; set; }
		/// <summary>
        /// 訊息傳送範本代碼
        /// </summary>
        public string? messageTemplateID { get; set; }
		/// <summary>
        /// 訊息傳送名稱
        /// </summary>
        public string? messageName { get; set; }

    }
}

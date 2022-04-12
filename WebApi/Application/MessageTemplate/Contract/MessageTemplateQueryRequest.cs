

namespace ESUN.AGD.WebApi.Application.MessageTemplate.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class MessageTemplateQueryRequest : CommonQuery
    {
		/// <summary>
        /// 訊息傳送範本代碼
        /// </summary>
        public string? messageTemplateID { get; set; }
		/// <summary>
        /// 訊息傳送範本名稱
        /// </summary>
        public string? messageTemplateName { get; set; }
		/// <summary>
        /// 訊息傳送B08代碼
        /// </summary>
        public string? messageB08Code { get; set; }

    }
}

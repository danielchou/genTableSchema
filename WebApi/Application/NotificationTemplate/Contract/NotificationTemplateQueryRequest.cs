

namespace ESUN.AGD.WebApi.Application.NotificationTemplate.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class NotificationTemplateQueryRequest : CommonQuery
    {
		/// <summary>
        /// 通知公告類別
        /// </summary>
        public string? notificationType { get; set; }
		/// <summary>
        /// 通知公告代碼
        /// </summary>
        public string? notificationID { get; set; }
		/// <summary>
        /// 通知公告名稱
        /// </summary>
        public string? notificationName { get; set; }

    }
}

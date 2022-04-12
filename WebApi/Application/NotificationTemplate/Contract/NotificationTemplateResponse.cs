﻿namespace ESUN.AGD.WebApi.Application.NotificationTemplate.Contract
{
    /// <summary>
    /// 通知公告範本回傳結果
    /// </summary>
    public class  NotificationTemplateResponse : CommonResponse
    {
        /// <summary>
        /// 通知公告類別
        /// </summary>
        public string notificationType { get; set; }
        /// <summary>
        /// 通知公告代碼
        /// </summary>
        public string notificationID { get; set; }
        /// <summary>
        /// 通知公告名稱
        /// </summary>
        public string notificationName { get; set; }
        /// <summary>
        /// 通知公告範本
        /// </summary>
        public string? content { get; set; }
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

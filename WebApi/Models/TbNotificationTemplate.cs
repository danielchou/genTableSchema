namespace ESUN.AGD.WebApi.Models
{
    public partial class TbNotificationTemplate : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 通知公告類別
        /// </summary>
        public string NotificationType { get; set; }  = null!;
        /// <summary>
        /// 通知公告代碼
        /// </summary>
        public string NotificationID { get; set; }  = null!;
        /// <summary>
        /// 通知公告名稱
        /// </summary>
        public string NotificationName { get; set; }  = null!;
        /// <summary>
        /// 通知公告範本
        /// </summary>
        public string? Content { get; set; } 
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


namespace ESUN.AGD.WebApi.Models
{
    public partial class TbMessageTemplate : CommonModel
    {
        
        /// <summary>
        /// 流水號
        /// </summary>
        public int SeqNo { get; set; } 
        /// <summary>
        /// 訊息傳送範本代碼
        /// </summary>
        public string MessageTemplateID { get; set; }  = null!;
        /// <summary>
        /// 訊息傳送範本名稱
        /// </summary>
        public string MessageTemplateName { get; set; }  = null!;
        /// <summary>
        /// 訊息傳送B08代碼
        /// </summary>
        public string MessageB08Code { get; set; }  = null!;
        /// <summary>
        /// 訊息傳送範本
        /// </summary>
        public string? Content { get; set; } 
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


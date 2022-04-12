namespace ESUN.AGD.WebApi.Application.MessageTemplate.Contract
{
    /// <summary>
    /// 電腦電話新增請求
    /// </summary>
    public class MessageTemplateInsertRequest : CommonInsertRequest
    {
        /// <summary>
        /// 訊息傳送範本代碼
        /// </summary>
        public string messageTemplateID { get; set; }
        /// <summary>
        /// 訊息傳送範本名稱
        /// </summary>
        public string messageTemplateName { get; set; }
        /// <summary>
        /// 訊息傳送B08代碼
        /// </summary>
        public string messageB08Code { get; set; }
        /// <summary>
        /// 訊息傳送範本
        /// </summary>
        public string? content { get; set; }
        /// <summary>
        /// 是否啟用?
        /// </summary>
        public bool isEnable { get; set; }
    }
}
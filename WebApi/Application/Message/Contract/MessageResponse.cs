namespace ESUN.AGD.WebApi.Application.Message.Contract
{
    /// <summary>
    /// 訊息傳送回傳結果
    /// </summary>
    public class  MessageResponse : CommonResponse
    {
        /// <summary>
        /// 訊息傳送頁籤代碼
        /// </summary>
        public string messageSheetID { get; set; }
        /// <summary>
        /// 訊息傳送範本代碼
        /// </summary>
        public string messageTemplateID { get; set; }
        /// <summary>
        /// 訊息傳送名稱
        /// </summary>
        public string messageName { get; set; }
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

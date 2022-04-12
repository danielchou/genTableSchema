namespace ESUN.AGD.WebApi.Application.Message.Contract
{
    /// <summary>
    /// 訊息傳送更新請求
    /// </summary>
    public class MessageUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
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

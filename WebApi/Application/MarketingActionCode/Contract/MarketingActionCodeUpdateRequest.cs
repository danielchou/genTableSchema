namespace ESUN.AGD.WebApi.Application.MarketingActionCode.Contract
{
    /// <summary>
    /// 行銷方案結果更新請求
    /// </summary>
    public class MarketingActionCodeUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 客群方案類別
        /// </summary>
        public string actionCodeType { get; set; }
		/// <summary>
        /// 行銷方案代碼
        /// </summary>
        public string marketingID { get; set; }
		/// <summary>
        /// 行銷結果代碼
        /// </summary>
        public string actionCode { get; set; }
		/// <summary>
        /// 行銷結果說明
        /// </summary>
        public string content { get; set; }
		/// <summary>
        /// 是否接受?
        /// </summary>
        public bool isAccept { get; set; }
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

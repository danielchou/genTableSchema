namespace ESUN.AGD.WebApi.Application.SysConfig.Contract
{
    /// <summary>
    /// 系統參數更新請求
    /// </summary>
    public class SysConfigUpdateRequest : CommonUpdateRequest
    {
		/// <summary>
        /// 流水號
        /// </summary>
        public int seqNo { get; set; }
		/// <summary>
        /// 系統參數類別
        /// </summary>
        public string sysConfigType { get; set; }
		/// <summary>
        /// 系統參數代碼
        /// </summary>
        public string sysConfigID { get; set; }
		/// <summary>
        /// 系統參數名稱
        /// </summary>
        public string sysConfigName { get; set; }
		/// <summary>
        /// 系統參數內容
        /// </summary>
        public string content { get; set; }
		/// <summary>
        /// 是否顯示?
        /// </summary>
        public bool isVisible { get; set; }
    }
}

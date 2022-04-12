

namespace ESUN.AGD.WebApi.Application.SysConfig.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class SysConfigQueryRequest : CommonQuery
    {
		/// <summary>
        /// 系統參數類別
        /// </summary>
        public string? sysConfigType { get; set; }
		/// <summary>
        /// 系統參數代碼
        /// </summary>
        public string? sysConfigID { get; set; }
		/// <summary>
        /// 系統參數名稱
        /// </summary>
        public string? sysConfigName { get; set; }

    }
}

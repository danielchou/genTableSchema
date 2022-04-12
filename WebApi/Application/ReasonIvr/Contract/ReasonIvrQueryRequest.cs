

namespace ESUN.AGD.WebApi.Application.ReasonIvr.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class ReasonIvrQueryRequest : CommonQuery
    {
		/// <summary>
        /// Ivr代碼
        /// </summary>
        public string? ivrID { get; set; }
		/// <summary>
        /// 原因代碼
        /// </summary>
        public string? reasonID { get; set; }

    }
}

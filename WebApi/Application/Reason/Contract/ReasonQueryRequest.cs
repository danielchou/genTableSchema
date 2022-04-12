

namespace ESUN.AGD.WebApi.Application.Reason.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class ReasonQueryRequest : CommonQuery
    {
		/// <summary>
        /// 聯繫原因碼代碼
        /// </summary>
        public string? reasonID { get; set; }
		/// <summary>
        /// 聯繫原因碼名稱
        /// </summary>
        public string? reasonName { get; set; }
		/// <summary>
        /// 上層聯繫原因碼代碼
        /// </summary>
        public string? parentReasonID { get; set; }
		/// <summary>
        /// 階層
        /// </summary>
        public int level { get; set; }

    }
}

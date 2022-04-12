

namespace ESUN.AGD.WebApi.Application.AuxCode.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class AuxCodeQueryRequest : CommonQuery
    {
		/// <summary>
        /// 休息碼代碼
        /// </summary>
        public string? auxID { get; set; }
		/// <summary>
        /// 休息碼名稱
        /// </summary>
        public string? auxName { get; set; }

    }
}

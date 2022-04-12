

namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class CodeQueryRequest : CommonQuery
    {
		/// <summary>
        /// 共用碼類別
        /// </summary>
        public string? codeType { get; set; }
		/// <summary>
        /// 共用碼代碼
        /// </summary>
        public string? codeID { get; set; }
		/// <summary>
        /// 共用碼名稱
        /// </summary>
        public string? codeName { get; set; }

    }
}

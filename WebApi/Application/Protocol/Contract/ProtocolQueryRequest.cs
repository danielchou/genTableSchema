

namespace ESUN.AGD.WebApi.Application.Protocol.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class ProtocolQueryRequest : CommonQuery
    {
		/// <summary>
        /// 通路碼代碼
        /// </summary>
        public string? protocol { get; set; }
		/// <summary>
        /// 通路碼名稱
        /// </summary>
        public string? protocolName { get; set; }

    }
}



namespace ESUN.AGD.WebApi.Application.PcPhone.Contract
{
    /// <summary>
    /// 進階查詢查詢請求
    /// </summary>
    public class PcPhoneQueryRequest : CommonQuery
    {
		/// <summary>
        /// 電腦名稱
        /// </summary>
        public string? computerName { get; set; }
		/// <summary>
        /// 分機號碼
        /// </summary>
        public string? extCode { get; set; }

    }
}
